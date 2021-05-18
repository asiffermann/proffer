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
        /// <param name="ccRecipients">The CC email recipients.</param>
        /// <param name="bccRecipients">The BCC email recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyText">The body as plain text.</param>
        /// <param name="bodyHtml">The body as HTML.</param>
        /// <param name="attachments">The file attachments.</param>
        /// <param name="replyTo">The reply-to email address.</param>
        public async Task SendEmailAsync(IEmail email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(email.From.DisplayName, email.From.Email));

            if (email.ReplyTo != null)
            {
                message.ReplyTo.Add(new MailboxAddress(email.ReplyTo.DisplayName, email.ReplyTo.Email));
            }

            foreach (IEmailAddress recipient in email.Recipients)
            {
                message.To.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            foreach (IEmailAddress recipient in email.CcRecipients)
            {
                message.Cc.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            foreach (IEmailAddress recipient in email.BccRecipients)
            {
                message.Bcc.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            message.Subject = email.Subject;

            var builder = new BodyBuilder
            {
                TextBody = email.BodyText,
                HtmlBody = email.BodyHtml
            };

            foreach (IEmailAttachment attachment in email.Attachments)
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
