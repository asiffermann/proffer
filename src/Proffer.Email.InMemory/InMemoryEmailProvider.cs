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
        /// <param name="email">All informations about the email.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SendEmailAsync(IEmail email)
        {
            this.inMemoryEmailRepository.Save(new InMemoryEmail
            {
                Subject = email.Subject,
                MessageText = email.BodyText,
                MessageHtml = email.BodyHtml,
                To = email.Recipients.ToArray(),
                Cc = email.CcRecipients.ToArray(),
                Bcc = email.BccRecipients.ToArray(),
                From = email.From,
                ReplyTo = email.ReplyTo,
                Attachments = email.Attachments
            });

            return Task.FromResult(0);
        }
    }
}
