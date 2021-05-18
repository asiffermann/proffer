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
        /// <param name="email">All informations about the email.</param>
        /// <exception cref="ArgumentException">Each email address should be unique between to, cc, and bcc recipients. We found duplicates.</exception>
        /// <exception cref="Exception">Cannot Send Email: {response.StatusCode}</exception>
        public async Task SendEmailAsync(IEmail email)
        {
            var allRecipients = new List<IEmailAddress>(email.Recipients);
            allRecipients.AddRange(email.CcRecipients);
            allRecipients.AddRange(email.BccRecipients);

            if (allRecipients.GroupBy(r => r.Email).Count() < allRecipients.Count)
            {
                throw new ArgumentException("Each email address should be unique between to, cc, and bcc recipients. We found duplicates.");
            }

            var client = new SendGridClient(this.apiKey);

            SendGridMessage message;

            if (email.Recipients.Count() == 1)
            {
                message = MailHelper.CreateSingleEmail(email.From.ToSendGridEmail(), email.Recipients.First().ToSendGridEmail(), email.Subject, email.BodyText, email.BodyHtml);
            }
            else
            {
                message = MailHelper.CreateSingleEmailToMultipleRecipients(
                    email.From.ToSendGridEmail(),
                    email.Recipients.Select(email => email.ToSendGridEmail()).ToList(),
                    email.Subject,
                    email.BodyText,
                    email.BodyHtml);
            }

            foreach (IEmailAddress ccRecipient in email.CcRecipients)
            {
                message.AddCc(ccRecipient.Email, ccRecipient.DisplayName);
            }

            foreach (IEmailAddress bccRecipient in email.BccRecipients)
            {
                message.AddBcc(bccRecipient.Email, bccRecipient.DisplayName);
            }

            if (email.Attachments.Any())
            {
                message.AddAttachments(email.Attachments.Select(a => new Attachment
                {
                    Filename = a.FileName,
                    Type = a.ContentType,
                    Content = Convert.ToBase64String(a.Data)
                }).ToList());
            }

            if (email.ReplyTo != null)
            {
                message.ReplyTo = email.ReplyTo.ToSendGridEmail();
            }

            Response response = await client.SendEmailAsync(message);
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Cannot Send Email: {response.StatusCode}");
            }
        }
    }
}
