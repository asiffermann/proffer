using System.Collections.Generic;

namespace Proffer.Email
{
    /// <summary>
    /// Contains the features that an email should have.
    /// </summary>
    public class Email : IEmail
    {
        /// <summary>
        /// The sender email address.
        /// </summary>
        public IEmailAddress From { get; set; }
        /// <summary>
        /// >The email recipients.
        /// </summary>
        public IEnumerable<IEmailAddress> Recipients { get; set; }
        /// <summary>
        /// The CC email recipients.
        /// </summary>
        public IEnumerable<IEmailAddress> CcRecipients { get; set; }
        /// <summary>
        /// The BCC email recipients.
        /// </summary>
        public IEnumerable<IEmailAddress> BccRecipients { get; set; }
        /// <summary>
        /// The subject.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The body as plain text.
        /// </summary>
        public string BodyText { get; set; }
        /// <summary>
        /// The body as HTML.
        /// </summary>
        public string BodyHtml { get; set; }
        /// <summary>
        /// The file attachments.
        /// </summary>
        public IEnumerable<IEmailAttachment> Attachments { get; set; }
        /// <summary>
        /// The reply-to email address.
        /// </summary>
        public IEmailAddress ReplyTo { get; set; }
        /// <summary>
        /// For template emails.
        /// </summary>
        public IDictionary<EmailTemplateType, string> TemplateDictionary { get; set; }
    }
}
