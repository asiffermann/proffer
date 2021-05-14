using System.Collections.Generic;

namespace Proffer.Email.Internal
{
    /// <summary>
    /// Builds an email object with additional features.
    /// </summary>
    public class EmailBuilder : IEmailBuilder
    {
        private IEmail email = new Email();

        /// <summary>
        /// Creating the email builder with required two paraters.
        /// </summary>
        /// <param name="From">Who sents mail</param>
        /// <param name="Recipients">Who gets email as first responsible</param>
        public EmailBuilder(IEmailAddress From, IEnumerable<IEmailAddress> Recipients)
        {
            this.email.From = From;
            this.email.Recipients = Recipients;
            this.email.ReplyTo = null;
            this.email.TemplateDictionary = new Dictionary<EmailTemplateType, string>
            {
                { EmailTemplateType.Subject, string.Empty},
                { EmailTemplateType.BodyText, string.Empty},
                { EmailTemplateType.BodyHtml, string.Empty},
            };
        }

        /// <summary>
        /// Replies an email.
        /// </summary>
        /// <param name="emailAddress">Email address</param>
        public EmailBuilder AddReplyTo(IEmailAddress emailAddress)
        {
            this.email.ReplyTo = emailAddress;
            return this;
        }

        /// <summary>
        /// Adds recipients to email.
        /// </summary>
        /// <param name="recipients">Recipient's email adressess</param>
        public EmailBuilder AddRecipient(IEnumerable<IEmailAddress> recipients)
        {
            this.email.Recipients = recipients;
            return this;
        }

        /// <summary>
        /// Adds ccrecipients to email.
        /// </summary>
        /// /// <param name="ccRecipients">CcRecipient's email adressess</param>
        public EmailBuilder AddCarbonCopyRecipient(IEnumerable<IEmailAddress> ccRecipients)
        {
            this.email.CcRecipients = ccRecipients;
            return this;
        }

        /// <summary>
        /// Adds bccrecipients to email.
        /// </summary>
        /// <param name="bccRecipients">BccRecipient's email adressess</param>
        public EmailBuilder AddBlackCarbonCopyRecipient(IEnumerable<IEmailAddress> bccRecipients)
        {
            this.email.BccRecipients = bccRecipients;
            return this;
        }

        /// <summary>
        /// Adds subject to email.
        /// </summary>
        /// <param name="subject">Mail's subject</param>
        public EmailBuilder AddSubject(string subject)
        {
            this.email.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds attachment to email.
        /// </summary>
        /// <param name="attachments">Mail's attachments</param>
        public EmailBuilder AddAttachment(IEnumerable<IEmailAttachment> attachments)
        {
            this.email.Attachments = attachments;
            return this;
        }

        /// <summary>
        /// Adds bodyhtml to email.
        /// </summary>
        /// <param name="bodyHtml">Mail's bodyHtml</param>
        public EmailBuilder AddBodyHtml(string bodyHtml)
        {
            this.email.BodyHtml = bodyHtml;
            return this;
        }

        /// <summary>
        /// Adds bodytext to email.
        /// </summary>
        /// <param name="bodyText">Mail's bodyText</param>
        public EmailBuilder AddBodyText(string bodyText)
        {
            this.email.BodyText = bodyText;
            return this;
        }

        /// <summary>
        /// Adds templatemail feature to email.
        /// </summary>
        /// <param name="templateKey">Sets mail's send with which template.</param>
        public EmailBuilder AddTemplate(string templateKey)
        {
            this.email.TemplateDictionary = new Dictionary<EmailTemplateType, string>()
            {
                { EmailTemplateType.Subject, templateKey},
                { EmailTemplateType.BodyText, templateKey },
                { EmailTemplateType.BodyHtml, templateKey},
            };

            return this;
        }

        IEmail IEmailBuilder.Build() => this.email;
    }
}
