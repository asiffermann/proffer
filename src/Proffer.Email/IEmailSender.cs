namespace Proffer.Email
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends templated or raw emails using configured providers.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The body as plain text.</param>
        /// <param name="to">The email recipients.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(string subject, string message, params IEmailAddress[] to);

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
        Task SendEmailAsync(IEmailAddress from, string subject, string message, params IEmailAddress[] to);

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
        Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, params IEmailAddress[] to);

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
        Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to);

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
        Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to);

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
        Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo = null, bool plainTextOnly = false);

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
        Task SendTemplatedEmailAsync<T>(string templateKey, T context, params IEmailAddress[] to);

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
        Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, params IEmailAddress[] to);

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
        Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, params IEmailAddress[] to);

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
        Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to);

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
        Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, params IEmailAddress[] to);

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
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo = null);
    }
}
