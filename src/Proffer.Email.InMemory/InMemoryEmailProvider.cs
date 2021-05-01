namespace Proffer.Email.InMemory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A provider which does not send any email but holds it in a collection in memory.
    /// </summary>
    /// <seealso cref="IEmailProvider" />
    public class InMemoryEmailProvider : IEmailProvider
    {
        private readonly IInMemoryEmailRepository inMemoryEmailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEmailProvider"/> class.
        /// </summary>
        /// <param name="inMemoryEmailRepository">The in-memory email repository.</param>
        public InMemoryEmailProvider(IInMemoryEmailRepository inMemoryEmailRepository)
        {
            this.inMemoryEmailRepository = inMemoryEmailRepository;
        }

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
        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml)
            => this.SendEmailAsync(from, recipients, subject, bodyText, bodyHtml, Enumerable.Empty<IEmailAttachment>());

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
        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments)
            => this.SendEmailAsync(from, recipients, Enumerable.Empty<IEmailAddress>(), Enumerable.Empty<IEmailAddress>(), subject, bodyText, bodyHtml, Enumerable.Empty<IEmailAttachment>());

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
        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo = null)
        {
            this.inMemoryEmailRepository.Save(new InMemoryEmail
            {
                Subject = subject,
                MessageText = bodyText,
                MessageHtml = bodyHtml,
                To = recipients.ToArray(),
                Cc = ccRecipients.ToArray(),
                Bcc = bccRecipients.ToArray(),
                From = from,
                ReplyTo = replyTo,
                Attachments = attachments
            });

            return Task.FromResult(0);
        }
    }
}
