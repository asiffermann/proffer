namespace Proffer.Email.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dawn;
    using Microsoft.Extensions.Options;
    using Storage;
    using Templating;

    /// <summary>
    /// Sends templated or raw emails using configured providers.
    /// </summary>
    /// <seealso cref="IEmailSender" />
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions options;
        private readonly IEmailProvider provider;
        private readonly ITemplateLoader templateLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="emailProviderTypes">The email provider types.</param>
        /// <param name="options">The Proffer.Email options.</param>
        /// <param name="storageFactory">The storage factory.</param>
        /// <param name="templateLoaderFactory">The template loader factory.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmailSender(
            IEnumerable<IEmailProviderType> emailProviderTypes,
            IOptions<EmailOptions> options,
            IStorageFactory storageFactory,
            ITemplateLoaderFactory templateLoaderFactory)
        {
            this.options = Guard.Argument(options.Value, nameof(EmailOptions))
                .NotNull()
                .Member(
                    o => o.Provider,
                    a => a
                        .NotNull()
                        .Member(po => po.Type, pa => pa.NotNull().NotEmpty()))
                .Value;

            IEmailProviderType providerType = emailProviderTypes
                .FirstOrDefault(x => x.Name == this.options.Provider.Type);
            if (providerType == null)
            {
                throw new ArgumentNullException("ProviderType", $"The provider type {this.options.Provider.Type} does not exist. Maybe you are missing a reference or an Add method call in your Startup class.");
            }

            this.provider = providerType.BuildProvider(this.options.Provider);

            if (!string.IsNullOrWhiteSpace(this.options.TemplateStorage))
            {
                IStore store = storageFactory.GetStore(this.options.TemplateStorage);
                this.templateLoader = templateLoaderFactory.Create(store);
            }
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">Everything about the mail.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmail email)
        {
            return this.DoMockupAndSendEmailAsync(email);
        }

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="email">Everything about the mail.</param>
        /// <param name="context">The context  to apply on the template.</param>
        public async Task SendTemplatedEmailAsync<T>(IEmail email, T context)
        {
            ITemplate subjectTemplate = await this.GetTemplateAsync(email.TemplateDictionary[EmailTemplateType.Subject], EmailTemplateType.Subject);
            email.Subject = subjectTemplate.Apply(context);

            ITemplate textTemplate = await this.GetTemplateAsync(email.TemplateDictionary[EmailTemplateType.BodyText], EmailTemplateType.BodyText);
            email.BodyText = textTemplate.Apply(context);

            ITemplate htmlTemplate = await this.GetTemplateAsync(email.TemplateDictionary[EmailTemplateType.BodyHtml], EmailTemplateType.BodyHtml);
            email.BodyHtml = htmlTemplate.Apply(context);

            await this.DoMockupAndSendEmailAsync(email);
        }

        /// <summary>
        /// Gets the template asynchronous.
        /// </summary>
        /// <param name="templateKey">The template key.</param>
        /// <param name="templateType">Type of the template.</param>
        /// <returns></returns>
        protected virtual Task<ITemplate> GetTemplateAsync(string templateKey, EmailTemplateType templateType)
            => this.templateLoader.GetTemplate($"{templateKey}-{templateType}");

        private IEnumerable<IEmailAddress> MockRecipients(IEnumerable<IEmailAddress> recipients, ICollection<IEmailAddress> alreadyMockedUpRecipients)
        {
            var finalRecipients = new List<IEmailAddress>();
            if (this.options.Mockup.Recipients.Any() && !string.IsNullOrEmpty(this.options.Mockup.Recipients.First()))
            {
                foreach (IEmailAddress recipient in recipients)
                {
                    var domain = new System.Net.Mail.MailAddress(recipient.Email).Host;

                    if (!this.options.Mockup.Exceptions.Emails.Contains(recipient.Email)
                        && !this.options.Mockup.Exceptions.Domains.Contains(domain))
                    {
                        if (!alreadyMockedUpRecipients.Any())
                        {
                            foreach (string mockupRecipient in this.options.Mockup.Recipients)
                            {
                                finalRecipients.Add(new EmailAddress(mockupRecipient, "Mockup Recipient"));
                            }
                        }

                        if (!alreadyMockedUpRecipients.Any(a => a.DisplayName == recipient.DisplayName && a.Email == recipient.Email))
                        {
                            alreadyMockedUpRecipients.Add(recipient);
                        }
                    }
                    else
                    {
                        finalRecipients.Add(recipient);
                    }
                }
            }
            else
            {
                finalRecipients = recipients.ToList();
            }

            return finalRecipients;
        }

        private async Task DoMockupAndSendEmailAsync(IEmail email)
        {
            var mockedUpRecipients = new List<IEmailAddress>();

            email.Recipients = this.MockRecipients(email.Recipients, mockedUpRecipients);
            email.CcRecipients = this.MockRecipients(email.CcRecipients, mockedUpRecipients);
            email.BccRecipients = this.MockRecipients(email.BccRecipients, mockedUpRecipients);

            if (mockedUpRecipients.Any())
            {
                string disclaimer = this.options.Mockup.Disclaimer;
                string joinedMockedUpRecipients = string.Join(", ", mockedUpRecipients.Select(r => $"{r.DisplayName} ({r.Email})"));

                email.BodyText = string.Concat(email.BodyText, Environment.NewLine, disclaimer, Environment.NewLine, joinedMockedUpRecipients);
                if (email.BodyHtml != null)
                {
                    email.BodyHtml = string.Concat(email.BodyHtml, "<br/><i>", disclaimer, "<br/>", joinedMockedUpRecipients, "</i>");
                }
            }

            await this.provider.SendEmailAsync(email);
        }
    }
}
