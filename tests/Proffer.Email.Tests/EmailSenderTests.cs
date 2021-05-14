namespace Proffer.Email.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Email;
    using Proffer.Email.Internal;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Email))]
    [Feature(nameof(EmailSender))]
    [Collection(nameof(EmailTestCollection))]
    public class EmailSenderTests
    {
        private readonly EmailFixture fixture;
        private readonly IEmailSender emailSender;
        private readonly EmailOptions options;

        public EmailSenderTests(EmailFixture fixture)
        {
            this.fixture = fixture;
            this.emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            this.options = fixture.Services.GetRequiredService<IOptions<EmailOptions>>().Value;
        }

        [Fact]
        public void Should_Throw_When_ResolvingUnknowProvider()
        {
            string sectionName = "CustomEmailSection";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddHandlebars()
                        .AddEmail(fixture.Configuration, sectionName)
                        .AddStubEmail();
                },
                new()
                {
                    { $"{sectionName}:Provider:Type", "UnknowProvider" }
                });

            Assert.Throws<ArgumentNullException>(() => fixture.Services.GetRequiredService<IEmailSender>());
        }

        [Fact]
        public void Should_Throw_When_ResolvingUnknowTemplatesStore()
        {
            string sectionName = "CustomEmailSection";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddHandlebars()
                        .AddEmail(fixture.Configuration, sectionName)
                        .AddStubEmail();
                },
                new()
                {
                    { $"{sectionName}:Provider:Type", "Stub" },
                    { $"{sectionName}:TemplateStorage", "UnknowStore" }
                });

            Assert.Throws<Storage.Exceptions.StoreNotFoundException>(() => fixture.Services.GetRequiredService<IEmailSender>());
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

        [Fact]
        [Bug("https://github.com/asiffermann/proffer/issues/102")]
        public async Task Should_SendEmailAsync_With_Mockup()
        {
            string defaultDisclaimer = "This email was originally destined to the following recipients, and was mocked up because it was sent from a test environment.";
            string mockupEmail = "mockup@getproffer.net";

            var fixture = new EmailFixture(new()
            {
                { "Email:Mockup:Recipients:0", mockupEmail }
            });

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, recipient.Yield())
                                             .AddSubject(subject)
                                             .AddBodyText(message);

            await emailSender.SendEmailAsync(emailBuilder.Build());

            emailBuilder.AddBodyText($"{message}{Environment.NewLine}{defaultDisclaimer}{Environment.NewLine}{recipient.DisplayName} ({recipient.Email})")
                        .AddBodyHtml($"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}</body></html><br/><i>{defaultDisclaimer}<br/>{recipient.DisplayName} ({recipient.Email})</i>")
                        .AddRecipient(new EmailAddress(mockupEmail, "Mockup Recipient").Yield());

            fixture.Verify(emailBuilder.Build());
        }

        [Theory]
        [Bug("https://github.com/asiffermann/proffer/issues/103")]
        [InlineData("bademail.com")]
        [InlineData("joe@jim@bademail.com")]
        [InlineData("@bademail.com")]
        [InlineData("bademail@")]
        [InlineData("@")]
        // TODO: [https://github.com/asiffermann/proffer/issues/103] Add more inline data with bad emails + different emails arguments
        public async Task Should_Throw_With_BadEmail(string badEmail)
        {
            string mockupEmail = "mockup@getproffer.net";

            var fixture = new EmailFixture(new()
            {
                { "Email:Mockup:Recipients:0", mockupEmail }
            });

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress(badEmail, "Recipient");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, recipient.Yield())
                                             .AddSubject(subject)
                                             .AddBodyText(message);

            await Assert.ThrowsAsync<FormatException>(() => emailSender.SendEmailAsync(emailBuilder.Build()));
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_MockupDomainException()
        {
            string mockupEmail = "mockup@getproffer.net";

            var fixture = new EmailFixture(new()
            {
                { "Email:Mockup:Recipients:0", mockupEmail },
                { "Email:Mockup:Exceptions:Domains:0", "getproffer.net" },
            });

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, recipient.Yield())
                                             .AddSubject(subject)
                                             .AddBodyText(message);

            await emailSender.SendEmailAsync(emailBuilder.Build());

            fixture.Verify(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_MockupEmailException()
        {
            string mockupEmail = "mockup@getproffer.net";

            var firstRecipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");
            var secondRecipient = new EmailAddress("recipient-2@getproffer.net", "Second recipient");

            var fixture = new EmailFixture(new()
            {
                { "Email:Mockup:Recipients:0", mockupEmail },
                { "Email:Mockup:Exceptions:Emails:0", firstRecipient.Email },
            });

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";

            IEmailBuilder emailBuilder = new EmailBuilder(this.options.DefaultSender, new List<IEmailAddress> { firstRecipient , secondRecipient})
                                             .AddSubject(subject)
                                             .AddBodyText(message);

            await emailSender.SendEmailAsync(emailBuilder.Build());

            emailBuilder.AddRecipient(new List<IEmailAddress> { firstRecipient, new EmailAddress(mockupEmail, "Mockup Recipient") });

            fixture.Verify(emailBuilder.Build());
        }
    }
}
