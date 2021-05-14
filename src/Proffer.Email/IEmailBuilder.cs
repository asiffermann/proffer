using System.Collections.Generic;
using Proffer.Email.Internal;

namespace Proffer.Email
{
    /// <summary>
    /// Builds an email object with additional features.
    /// </summary>
    public interface IEmailBuilder
    {
        /// <summary>
        /// Returns existing email object
        /// </summary>
        /// <returns></returns>
        public IEmail Build();

        /// <summary>
        /// Adding reply to feature to email object
        /// </summary>
        /// <param name="emailAddress"> Email Address</param>
        /// <returns></returns>
        public EmailBuilder AddReplyTo(IEmailAddress emailAddress);
        /// <summary>
        /// Adding recipents to email object
        /// </summary>
        /// <param name="recipients"> Recipients</param>
        public EmailBuilder AddRecipient(IEnumerable<IEmailAddress> recipients);
        /// <summary>
        /// Adding ccrecipents to email object
        /// </summary>
        /// <param name="ccRecipients"> Cc Recipients</param>
        public EmailBuilder AddCarbonCopyRecipient(IEnumerable<IEmailAddress> ccRecipients);
        /// <summary>
        /// Adding bccrecipents to email object
        /// </summary>
        /// <param name="bccRecipients"> Bcc Recipients</param>
        public EmailBuilder AddBlackCarbonCopyRecipient(IEnumerable<IEmailAddress> bccRecipients);
        /// <summary>
        /// Adding subject to email object
        /// </summary>
        /// <param name="subject"> Subject</param>
        public EmailBuilder AddSubject(string subject);
        /// <summary>
        /// Adding attachment to email object
        /// </summary>
        /// <param name="attachments"> Attachment</param>
        public EmailBuilder AddAttachment(IEnumerable<IEmailAttachment> attachments);
        /// <summary>
        /// Adding bodyHtml to email object
        /// </summary>
        /// <param name="bodyHtml"> Body Html</param>
        public EmailBuilder AddBodyHtml(string bodyHtml);
        /// <summary>
        /// Adding bodyText to email object
        /// </summary>
        /// <param name="bodyText"> Body Text</param>
        public EmailBuilder AddBodyText(string bodyText);
        /// <summary>
        /// Adding template feature to email object
        /// </summary>
        /// <param name="templateKey"> Template Key</param>
        public EmailBuilder AddTemplate(string templateKey);
    }
}
