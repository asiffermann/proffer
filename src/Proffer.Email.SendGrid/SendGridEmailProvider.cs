namespace Proffer.Email.SendGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::SendGrid;
    using global::SendGrid.Helpers.Mail;

    /// <summary>
    /// A provider that sends email using <see cref="global::SendGrid"/> API.
    /// </summary>
    /// <seealso cref="IEmailProvider" />
    public class SendGridEmailProvider : IEmailProvider
    {
        private readonly string apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridEmailProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">apiKey</exception>
        public SendGridEmailProvider(IEmailProviderOptions options)
        {
            this.apiKey = options.Parameters["Key"];

            if (string.IsNullOrWhiteSpace(this.apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }
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
        /// <exception cref="ArgumentException">Each email address should be unique between to, cc, and bcc recipients. We found duplicates.</exception>
        /// <exception cref="Exception">Cannot Send Email: {response.StatusCode}</exception>
        public async Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo = null)
        {
            var allRecipients = new List<IEmailAddress>(recipients);
            allRecipients.AddRange(ccRecipients);
            allRecipients.AddRange(bccRecipients);

            if (allRecipients.GroupBy(r => r.Email).Count() < allRecipients.Count)
            {
                throw new ArgumentException("Each email address should be unique between to, cc, and bcc recipients. We found duplicates.");
            }

            var client = new SendGridClient(this.apiKey);

            SendGridMessage message;

            if (recipients.Count() == 1)
            {
                message = MailHelper.CreateSingleEmail(from.ToSendGridEmail(), recipients.First().ToSendGridEmail(), subject, bodyText, bodyHtml);
            }
            else
            {
                message = MailHelper.CreateSingleEmailToMultipleRecipients(
                    from.ToSendGridEmail(),
                    recipients.Select(email => email.ToSendGridEmail()).ToList(),
                    subject,
                    bodyText,
                    bodyHtml);
            }

            foreach (IEmailAddress ccRecipient in ccRecipients)
            {
                message.AddCc(ccRecipient.Email, ccRecipient.DisplayName);
            }

            foreach (IEmailAddress bccRecipient in bccRecipients)
            {
                message.AddBcc(bccRecipient.Email, bccRecipient.DisplayName);
            }

            if (attachments.Any())
            {
                message.AddAttachments(attachments.Select(a => new Attachment
                {
                    Filename = a.FileName,
                    Type = a.ContentType,
                    Content = Convert.ToBase64String(a.Data)
                }).ToList());
            }

            if (replyTo != null)
            {
                message.ReplyTo = replyTo.ToSendGridEmail();
            }

            Response response = await client.SendEmailAsync(message);
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Cannot Send Email: {response.StatusCode}");
            }
        }
    }
}
