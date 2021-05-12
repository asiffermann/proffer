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
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(string subject, string message, params IEmailAddress[] to)
            => this.SendEmailAsync(this.options.DefaultSender, subject, message, to);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmailAddress from, string subject, string message, params IEmailAddress[] to)
            => this.SendEmailAsync(from, subject, message, Enumerable.Empty<IEmailAttachment>(), to);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="plainTextOnly">If set to <c>true</c> the body shoud be sent as plain text only.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, params IEmailAddress[] to)
            => this.SendEmailAsync(from, replyTo, subject, message, plainTextOnly, Enumerable.Empty<IEmailAttachment>(), to);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to)
            => this.SendEmailAsync(from, subject, message, attachments, to.ToArray(), new IEmailAddress[0], new IEmailAddress[0]);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="plainTextOnly">If set to <c>true</c> the body shoud be sent as plain text only.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to)
            => this.SendEmailAsync(from, subject, message, attachments, to.ToArray(), new IEmailAddress[0], new IEmailAddress[0], replyTo: replyTo, plainTextOnly: plainTextOnly);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <param name="cc">The CC email recipients.</param>
        /// <param name="bcc">The BCC email recipients.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <param name="plainTextOnly">If set to <c>true</c> the body shoud be sent as plain text only.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo = null, bool plainTextOnly = false)
        {
            if (plainTextOnly)
            {
                return this.DoMockupAndSendEmailAsync(
                 from,
                 replyTo,
                 to,
                 cc,
                 bcc,
                 subject,
                 message,
                 null,
                 attachments);
            }

            return this.DoMockupAndSendEmailAsync(
              from,
              replyTo,
              to,
              cc,
              bcc,
              subject,
              message,
              $"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}</body></html>",
              attachments);
        }

        /// <summary>
        /// Sends a templated email from the configured default sender email address.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendTemplatedEmailAsync<T>(string templateKey, T context, params IEmailAddress[] to)
            => this.SendTemplatedEmailAsync(this.options.DefaultSender, templateKey, context, to);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="from">The sender email address.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, params IEmailAddress[] to)
            => this.SendTemplatedEmailAsync(from, templateKey, context, Enumerable.Empty<IEmailAttachment>(), to);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="from">The sender email address.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, params IEmailAddress[] to)
            => this.SendTemplatedEmailAsync(from, replyTo, templateKey, context, Enumerable.Empty<IEmailAttachment>(), to);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="from">The sender email address.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to)
            => this.SendTemplatedEmailAsync(from, templateKey, context, attachments, to, new IEmailAddress[0], new IEmailAddress[0]);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="from">The sender email address.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to)
            => this.SendTemplatedEmailAsync(from, templateKey, context, attachments, to, new IEmailAddress[0], new IEmailAddress[0], replyTo: replyTo);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="from">The sender email address.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="to">The email recipients.</param>
        /// <param name="cc">The CC email recipients.</param>
        /// <param name="bcc">The BCC email recipients.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        public async Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo = null)
        {
            ITemplate subjectTemplate = await this.GetTemplateAsync(templateKey, EmailTemplateType.Subject);
            ITemplate textTemplate = await this.GetTemplateAsync(templateKey, EmailTemplateType.BodyText);
            ITemplate htmlTemplate = await this.GetTemplateAsync(templateKey, EmailTemplateType.BodyHtml);

            await this.DoMockupAndSendEmailAsync(
                from,
                replyTo,
                to,
                cc,
                bcc,
                subjectTemplate.Apply(context),
                textTemplate.Apply(context),
                htmlTemplate.Apply(context),
                attachments);
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
                    string[] emailParts = recipient.Email.Split('@');
                    if (emailParts.Length != 2)
                    {
                        throw new NotSupportedException("Bad recipient email.");
                    }

                    string domain = emailParts[1];

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

        private async Task DoMockupAndSendEmailAsync(
            IEmailAddress from,
            IEmailAddress replyTo,
            IEnumerable<IEmailAddress> recipients,
            IEnumerable<IEmailAddress> ccRecipients,
            IEnumerable<IEmailAddress> bccRecipients,
            string subject,
            string text,
            string html,
            IEnumerable<IEmailAttachment> attachments)
        {
            var mockedUpRecipients = new List<IEmailAddress>();

            IEnumerable<IEmailAddress> finalToRecipients = this.MockRecipients(recipients, mockedUpRecipients);
            IEnumerable<IEmailAddress> finalCcRecipients = this.MockRecipients(ccRecipients, mockedUpRecipients);
            IEnumerable<IEmailAddress> finalBccRecipients = this.MockRecipients(bccRecipients, mockedUpRecipients);

            if (mockedUpRecipients.Any())
            {
                string disclaimer = this.options.Mockup.Disclaimer;
                string joinedMockedUpRecipients = string.Join(", ", mockedUpRecipients.Select(r => $"{r.DisplayName} ({r.Email})"));

                text = string.Concat(text, Environment.NewLine, disclaimer, Environment.NewLine, joinedMockedUpRecipients);
                if (html != null)
                {
                    html = string.Concat(html, "<br/><i>", disclaimer, "<br/>", joinedMockedUpRecipients, "</i>");
                }
            }

            await this.provider.SendEmailAsync(
                from,
                finalToRecipients,
                finalCcRecipients,
                finalBccRecipients,
                subject,
                text,
                html,
                attachments,
                replyTo: replyTo);
        }
    }
}
