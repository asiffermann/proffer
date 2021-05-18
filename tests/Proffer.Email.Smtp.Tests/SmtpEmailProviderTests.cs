namespace Proffer.Email.Smtp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Proffer.Email;
    using Proffer.Email.Internal;
    using Proffer.Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Email))]
    [Feature(nameof(Smtp))]
    [Feature(nameof(SmtpEmailProvider))]
    [Collection(nameof(SmtpTestCollection))]
    public class SmtpEmailProviderTests : IAsyncLifetime
    {
        private readonly SmtpFixture fixture;
        private readonly IEmailSender emailSender;
        private readonly EmailOptions options;
        private IHost host;

        public SmtpEmailProviderTests(SmtpFixture fixture)
        {
            this.fixture = fixture;
            this.emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            this.options = fixture.Services.GetRequiredService<IOptions<EmailOptions>>().Value;
        }

        public async Task InitializeAsync()
        {
            this.host = await this.fixture.RunSmtpServer();
        }

        [Fact]
        public async Task Should_SendEmailAsync()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
               .AddSubject("Hello!")
               .AddBodyText("Message in plain text");

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            emailBuilder.AddBodyHtml($"<!DOCTYPE html><html><head><title>{emailBuilder.Build().Subject}</title></head><body>{emailBuilder.Build().BodyText}</body></html>");
            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_Attachments()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            IFileReference beach = await this.fixture.Attachments.GetAsync("beach.jpeg", withMetadata: true);
            IFileReference sample = await this.fixture.Attachments.GetAsync("sample.pdf", withMetadata: true);

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                .AddSubject("Hello!")
                .AddBodyText("Message in plain text")
                .AddAttachment(new List<IEmailAttachment>
                {
                    new EmailAttachment(beach.Path, await beach.ReadAllBytesAsync(), beach.Properties.ContentType),
                    new EmailAttachment(sample.Path, await sample.ReadAllBytesAsync(), sample.Properties.ContentType.Split('/')[0], sample.Properties.ContentType.Split('/')[1]),
                });


            await this.emailSender.SendEmailAsync(emailBuilder.Build());
            emailBuilder.AddBodyHtml($"<!DOCTYPE html><html><head><title>{emailBuilder.Build().Subject}</title></head><body>{emailBuilder.Build().BodyText}</body></html>");

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_CarbonCopyRecipients()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            var carbonCopyRecipient = new EmailAddress("cc-recipient@getproffer.net", "Carbon-Copy Recipient");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                                            .AddCarbonCopyRecipient(new List<EmailAddress>() { carbonCopyRecipient })
                                            .AddSubject("Hello!")
                                            .AddBodyText("Message in plain text");

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            emailBuilder.AddBodyHtml($"<!DOCTYPE html><html><head><title>{emailBuilder.Build().Subject}</title></head><body>{emailBuilder.Build().BodyText}</body></html>");

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_BlackCarbonCopyRecipients()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            var blackCarbonCopyRecipient = new EmailAddress("cc-recipient@getproffer.net", "Carbon-Copy Recipient");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                                            .AddBlackCarbonCopyRecipient(new List<EmailAddress>() { blackCarbonCopyRecipient })
                                            .AddSubject("Hello!")
                                            .AddBodyText("Message in plain text");

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            emailBuilder.AddBodyHtml($"<!DOCTYPE html><html><head><title>{emailBuilder.Build().Subject}</title></head><body>{emailBuilder.Build().BodyText}</body></html>");

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_PlainTextOnly()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                                            .AddSubject("Hello!")
                                            .AddBodyText("Message in plain text");

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_ReplyTo()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            var replyTo = new EmailAddress("custom-reply-to@getproffer.net", "Custom Reply-To");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                                            .AddSubject("Hello!")
                                            .AddBodyText("Message in plain text")
                                            .AddReplyTo(replyTo);

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_Sender()
        {
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            var sender = new EmailAddress("custom-sender@getproffer.net", "Custom Sender");

            IEmailBuilder emailBuilder = new EmailBuilder(sender, new List<EmailAddress>() { recipient })
                                            .AddSubject("Hello!")
                                            .AddBodyText("Message in plain text");

            await this.emailSender.SendEmailAsync(emailBuilder.Build());

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendTemplatedEmailAsync_With_KeyAndContext()
        {
            string templateKey = "TemplatedEmail";
            var context = new
            {
                Name = "Arsène Milhaud",
                Title = "This is a templated email, Señor!",
                RawHtml = "With <b>important</b> hightlights",
                Message = "With important hightlights too, but not in bold 🙃",
            };

            var recipient = new EmailAddress("recipient-1@getproffer.net", context.Name);

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<EmailAddress>() { recipient })
                                            .AddTemplate(templateKey);

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), context);

            emailBuilder.AddSubject($"Hello {context.Name}!")
                        .AddBodyHtml($"<h1>{WebUtility.HtmlEncode(context.Title)}</h1>{Environment.NewLine}{context.RawHtml}")
                        .AddBodyText($"{context.Title}{Environment.NewLine}{Environment.NewLine}{context.Message}");

            this.fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendTemplatedEmailAsync_With_SenderAndReplyTo()
        {
            string templateKey = "TemplatedEmail";
            var context = new
            {
                Name = "Arsène Milhaud",
                Title = "This is a templated email, Señor!",
                RawHtml = "With <b>important</b> hightlights",
                Message = "With important hightlights too, but not in bold 🙃",
            };

            var sender = new EmailAddress("custom-sender@getproffer.net", "Proffer");
            var replyTo = new EmailAddress("hello@getproffer.net", "Proffer");
            var recipient = new EmailAddress("recipient-1@getproffer.net", context.Name);

            IEmailBuilder emailBuilder = new EmailBuilder(sender, new List<EmailAddress>() { recipient })
                                            .AddTemplate(templateKey)
                                            .AddReplyTo(replyTo);

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), context);

            emailBuilder.AddSubject($"Hello {context.Name}!")
                        .AddBodyHtml($"<h1>{WebUtility.HtmlEncode(context.Title)}</h1>{Environment.NewLine}{context.RawHtml}")
                        .AddBodyText($"{context.Title}{Environment.NewLine}{Environment.NewLine}{context.Message}");

            this.fixture.Verify(emailBuilder.Build());
        }

        public async Task DisposeAsync()
        {
            await this.host.StopAsync();
            this.host.Dispose();
        }
    }
}
