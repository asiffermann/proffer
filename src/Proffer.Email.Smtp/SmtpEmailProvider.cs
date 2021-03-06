namespace Proffer.Email.Smtp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dawn;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.DependencyInjection;
    using MimeKit;

    /// <summary>
    /// A provider that sends email to a SMTP server with <see cref="MailKit"/>.
    /// </summary>
    /// <seealso cref="IEmailProvider" />
    public class SmtpEmailProvider : IEmailProvider
    {
        private readonly string host;
        private readonly int port;
        private readonly string username;
        private readonly string password;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailProvider" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SmtpEmailProvider(IServiceProvider serviceProvider, IEmailProviderOptions options)
        {
            this.host = Guard.Argument(options.Parameters["Host"], "Host").NotNull().NotEmpty().NotWhiteSpace();
            this.port = Guard.Argument(options.Parameters["Port"], "Port").NotNull().NotEmpty().NotWhiteSpace()
                .Modify((portString) =>
                {
                    if (int.TryParse(portString, out int port))
                    {
                        return port;
                    }

                    return (int?)null;
                })
                .NotNull()
                .Value;

            this.serviceProvider = serviceProvider;

            options.Parameters.TryGetValue("UserName", out this.username);
            options.Parameters.TryGetValue("Password", out this.password);
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
        public async Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from.DisplayName, from.Email));

            if (replyTo != null)
            {
                message.ReplyTo.Add(new MailboxAddress(replyTo.DisplayName, replyTo.Email));
            }

            foreach (IEmailAddress recipient in recipients)
            {
                message.To.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            foreach (IEmailAddress recipient in ccRecipients)
            {
                message.Cc.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            foreach (IEmailAddress recipient in bccRecipients)
            {
                message.Bcc.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            message.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = bodyText,
                HtmlBody = bodyHtml
            };

            foreach (IEmailAttachment attachment in attachments)
            {
                builder.Attachments.Add(attachment.FileName, attachment.Data, new ContentType(attachment.MediaType, attachment.MediaSubtype));
            }

            message.Body = builder.ToMessageBody();

            foreach (TextPart textBodyPart in message.BodyParts.OfType<TextPart>())
            {
                textBodyPart.ContentTransferEncoding = ContentEncoding.Base64;
            }

            using (ISmtpClient client = this.serviceProvider.GetRequiredService<ISmtpClient>())
            {
                await client.ConnectAsync(this.host, this.port, SecureSocketOptions.None);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(this.username))
                {
                    await client.AuthenticateAsync(this.username, this.password);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
