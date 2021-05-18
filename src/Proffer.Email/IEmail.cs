using System.Collections.Generic;

namespace Proffer.Email
{
    /// <summary>
    /// Contains the features that an email should have.
    /// </summary>
    public interface IEmail
    {
        /// <summary>
        /// The sender email address.
        /// </summary>
        IEmailAddress From { get; set; }
        /// <summary>
        /// >The email recipients.
        /// </summary>
        IEnumerable<IEmailAddress> Recipients { get; set; }
        /// <summary>
        /// The CC email recipients.
        /// </summary>
        IEnumerable<IEmailAddress> CcRecipients { get; set; }
        /// <summary>
        /// The BCC email recipients.
        /// </summary>
        IEnumerable<IEmailAddress> BccRecipients { get; set; }
        /// <summary>
        /// The subject.
        /// </summary>
        string Subject { get; set; }
        /// <summary>
        /// The body as plain text.
        /// </summary>
        string BodyText { get; set; }
        /// <summary>
        /// The body as HTML.
        /// </summary>
        string BodyHtml { get; set; }
        /// <summary>
        /// The file attachments.
        /// </summary>
        IEnumerable<IEmailAttachment> Attachments { get; set; }
        /// <summary>
        /// The reply-to email address.
        /// </summary>
        IEmailAddress ReplyTo { get; set; }
        /// <summary>
        /// For template emails.
        /// </summary>
        IDictionary<EmailTemplateType, string> TemplateDictionary { get; set; }
    }
}
