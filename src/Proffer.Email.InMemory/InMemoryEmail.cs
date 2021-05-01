namespace Proffer.Email.InMemory
{
    using System.Collections.Generic;

    /// <summary>
    /// An object to retain the values of the email that would have been sent.
    /// </summary>
    public class InMemoryEmail
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message as plain-text.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the message as HTML.
        /// </summary>
        public string MessageHtml { get; set; }

        /// <summary>
        /// Gets or sets the email recipients.
        /// </summary>
        public IEmailAddress[] To { get; set; }

        /// <summary>
        /// Gets or sets the email recipients.
        /// </summary>
        public IEmailAddress[] Cc { get; set; }

        /// <summary>
        /// Gets or sets the BCC email recipients.
        /// </summary>
        public IEmailAddress[] Bcc { get; set; }

        /// <summary>
        /// Gets or sets the sender email address.
        /// </summary>
        public IEmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the reply-to email address.
        /// </summary>
        public IEmailAddress ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the attachments files.
        /// </summary>
        public IEnumerable<IEmailAttachment> Attachments { get; set; }
    }
}
