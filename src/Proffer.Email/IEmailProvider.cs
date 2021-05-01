namespace Proffer.Email
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A provider sends email using a particular messaging protocol or API.
    /// </summary>
    public interface IEmailProvider
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="recipients">The email recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyText">The body as plain text.</param>
        /// <param name="bodyHtml">The body as HTML.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="recipients">The email recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyText">The body as plain text.</param>
        /// <param name="bodyHtml">The body as HTML.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">The sender email address.</param>
        /// <param name="recipients">The email recipients.</param>
        /// <param name="ccRecipients">The CC email recipients.</param>
        /// <param name="bccRecipients">The BCC email recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyText">The body as plain text.</param>
        /// <param name="bodyHtml">The body as HTML.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo = null);
    }
}
