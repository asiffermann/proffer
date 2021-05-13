namespace Proffer.Email.Smtp.Tests
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;
    using MimeKit;
    using Proffer.Email.Internal;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Testing;
    using SmtpServer.Authentication;
    using SmtpServer.Mail;
    using SmtpServer.Protocol;
    using SmtpServer.Storage;
    using Xunit;

    public class SmtpFixture : ServiceProviderFixtureBase
    {
        private readonly List<MimeMessage> emails = new();
        private readonly string smtpUserName = "SmtpUser";
        private readonly string smtpPassword = Guid.NewGuid().ToString();
        private readonly int smtpPort = new Random().Next(26100, 26800);

        public SmtpFixture()
        {
            IStorageFactory storageFactory = this.Services.GetRequiredService<IStorageFactory>();
            this.Attachments = storageFactory.GetStore("Attachments");
        }

        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        public IStore Attachments { get; }

        public void Verify(
            IEmailAddress sender = null,
            List<IEmailAddress> recipients = null,
            List<IEmailAddress> ccRecipients = null,
            List<IEmailAddress> bccRecipients = null,
            string subject = null,
            string bodyText = null,
            string bodyHtml = null,
            List<IEmailAttachment> attachments = null,
            IEmailAddress replyTo = null)
        {
            IOptions<EmailOptions> options = this.Services.GetRequiredService<IOptions<EmailOptions>>();

            sender ??= options.Value.DefaultSender;
            recipients ??= new();
            ccRecipients ??= new();
            bccRecipients ??= new();
            attachments ??= new();

            EmailAddressStrictEqualityComparer emailStrictComparer = new();
            EmailAddressEqualityComparer emailComparer = new();
            EmailAttachmentEqualityComparer attachmentComparer = new();

            bool emailsEqual(List<IEmailAddress> expected, IEnumerable<IEmailAddress> actual, EqualityComparerBase<IEmailAddress> comparer = null)
            {
                var firstNotSecond = actual.Except(expected, comparer ?? emailStrictComparer).ToList();
                var secondNotFirst = expected.Except(actual, comparer ?? emailStrictComparer).ToList();
                return !firstNotSecond.Any() && !secondNotFirst.Any();
            }

            bool attachmentsEqual(List<IEmailAttachment> expected, IEnumerable<IEmailAttachment> actual)
            {
                var firstNotSecond = actual.Except(expected, attachmentComparer).ToList();
                var secondNotFirst = expected.Except(actual, attachmentComparer).ToList();
                return !firstNotSecond.Any() && !secondNotFirst.Any();
            }

            var t = this.emails.First().Attachments.Select(a => ToEmailAttachment(a)).ToList();

            bool emailWasStored = this.emails
                .Where(e => emailStrictComparer.Equals(sender, ToEmailAddress(e.From.FirstOrDefault())))
                .Where(e => emailsEqual(recipients, e.To.Select(t => ToEmailAddress(t)).ToList()))
                .Where(e => emailsEqual(ccRecipients, e.Cc.Select(t => ToEmailAddress(t)).ToList()))
                .Where(e => emailsEqual(bccRecipients, e.Bcc.Select(t => ToEmailAddress(t)).ToList(), emailComparer))
                .Where(e => subject == null || subject == e.Subject)
                .Where(e => bodyText == null || bodyText == e.TextBody)
                .Where(e => bodyHtml == null || bodyHtml == e.HtmlBody)
                .Where(e => attachmentsEqual(attachments, e.Attachments.Select(a => ToEmailAttachment(a)).ToList()))
                .Where(e => replyTo == null || emailStrictComparer.Equals(replyTo, ToEmailAddress(e.ReplyTo.FirstOrDefault())))
                .Any();

            Assert.True(emailWasStored);
        }

        public async Task<IHost> RunSmtpServer()
        {
            string basePath = PlatformServices.Default.Application.ApplicationBasePath.TrimEnd('\\').TrimEnd('/');
            IConfigurationRoot configuration = null;

            IHost host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureAppConfiguration((builderContext, configBuilder) =>
                        {
                            configBuilder
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile("appsettings.development.json", optional: true)
                                .AddEnvironmentVariables();

                            configuration = configBuilder.Build();
                        })
                        .ConfigureServices((builderContext, services) =>
                        {
                            services.AddSingleton<IUserAuthenticator>(new SmtpServerUserAuthenticator(this.smtpUserName, this.smtpPassword));
                            services.AddSingleton<IMessageStore>(new SmtpServerMessageStore(this.emails));

                            services.AddSingleton(
                                provider =>
                                {
                                    SmtpServer.ISmtpServerOptions options = new SmtpServer.SmtpServerOptionsBuilder()
                                        .ServerName("SMTP Server")
                                        .Endpoint(builder =>
                                            builder
                                                .Port(this.smtpPort, false)
                                                .AllowUnsecureAuthentication()
                                                .AuthenticationRequired())
                                        .Build();

                                    return new SmtpServer.SmtpServer(options, provider.GetRequiredService<IServiceProvider>());
                                });

                            services.AddHostedService<Worker>();
                        })
                        .Configure(app =>
                        {
                            app.Run(async ctx => await ctx.Response.WriteAsync(""));
                        });
                })
                .StartAsync();

            return host;
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddFileSystemExtendedProperties()
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration)
                .AddSmtpEmail();
        }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            inMemoryCollectionData.Add("Email:Provider:Parameters:Port", this.smtpPort.ToString());
            inMemoryCollectionData.Add("Email:Provider:Parameters:UserName", this.smtpUserName);
            inMemoryCollectionData.Add("Email:Provider:Parameters:Password", this.smtpPassword);
        }

        private static IEmailAddress ToEmailAddress(InternetAddress internetAddress)
        {
            if (internetAddress == null || internetAddress is not MailboxAddress mailboxAddress)
            {
                return null;
            }

            return new EmailAddress(mailboxAddress.Address, mailboxAddress.Name);
        }

        private static IEmailAttachment ToEmailAttachment(MimeEntity mimeEntity)
        {
            if (mimeEntity == null || mimeEntity is not MimePart mimePart)
            {
                return null;
            }

            using (var memory = new MemoryStream())
            {
                mimePart.Content.DecodeTo(memory);
                return new EmailAttachment(mimePart.FileName, memory.ToArray(), mimePart.ContentType.MimeType);
            }
        }

        public class SmtpServerMessageStore : MessageStore
        {
            private readonly List<MimeMessage> store;

            public SmtpServerMessageStore(List<MimeMessage> store)
            {
                this.store = store;
            }

            public override async Task<SmtpResponse> SaveAsync(SmtpServer.ISessionContext context, SmtpServer.IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
            {
                await using var stream = new MemoryStream();

                SequencePosition position = buffer.GetPosition(0);
                while (buffer.TryGet(ref position, out ReadOnlyMemory<byte> memory))
                {
                    stream.Write(memory.Span);
                }

                stream.Position = 0;

                MimeMessage message = await MimeMessage.LoadAsync(stream, cancellationToken);

                IEnumerable<string> messageEmails = message.To.Union(message.Cc)
                    .OfType<MailboxAddress>()
                    .Select(ma => ma.Address.ToLower());

                IEnumerable<string> transactionEmails = transaction.To.Select(m => m.AsAddress().ToLower());

                IEnumerable<MailboxAddress> bccMailboxAddresses = transactionEmails
                    .Except(messageEmails)
                    .Select(s => MailboxAddress.Parse(s));

                message.Bcc.AddRange(bccMailboxAddresses);

                this.store.Add(message);

                return SmtpResponse.Ok;
            }
        }

        public sealed class SmtpServerUserAuthenticator : UserAuthenticator
        {
            private readonly string smtpUserName;
            private readonly string smtpPassword;

            public SmtpServerUserAuthenticator(string smtpUserName, string smtpPassword)
            {
                this.smtpUserName = smtpUserName;
                this.smtpPassword = smtpPassword;
            }

            public override Task<bool> AuthenticateAsync(
                SmtpServer.ISessionContext context,
                string user,
                string password,
                CancellationToken cancellationToken)
            {
                return Task.FromResult(user == this.smtpUserName && password == this.smtpPassword);
            }
        }

        public sealed class Worker : BackgroundService
        {
            private readonly SmtpServer.SmtpServer smtpServer;

            public Worker(SmtpServer.SmtpServer smtpServer)
            {
                this.smtpServer = smtpServer;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                return this.smtpServer.StartAsync(stoppingToken);
            }
        }
    }
}
