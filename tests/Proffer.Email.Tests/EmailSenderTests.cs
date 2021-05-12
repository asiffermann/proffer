namespace Proffer.Email.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    public class EmailSenderTests
    {
        public EmailSenderTests()
        {
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
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");

            await emailSender.SendEmailAsync(subject, message, recipient);

            fixture.Verify(
                subject: subject,
                bodyText: message,
                bodyHtml: $"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}</body></html>",
                recipients: new() { recipient });
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_Attachments()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            EmailOptions options = fixture.Services.GetRequiredService<IOptions<EmailOptions>>().Value;

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");

            IFileReference beach = await fixture.Attachments.GetAsync("beach.jpeg", withMetadata: true);
            IFileReference sample = await fixture.Attachments.GetAsync("sample.pdf", withMetadata: true);

            var attachments = new List<IEmailAttachment>
            {
                new EmailAttachment(beach.Path, await beach.ReadAllBytesAsync(), beach.Properties.ContentType),
                new EmailAttachment(sample.Path, await sample.ReadAllBytesAsync(), sample.Properties.ContentType.Split('/')[0], sample.Properties.ContentType.Split('/')[1]),
            };

            await emailSender.SendEmailAsync(options.DefaultSender, subject, message, attachments, recipient);

            fixture.Verify(
                subject: subject,
                bodyText: message,
                bodyHtml: $"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}</body></html>",
                recipients: new() { recipient },
                attachments: attachments);
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_PlainTextOnly()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            EmailOptions options = fixture.Services.GetRequiredService<IOptions<EmailOptions>>().Value;

            string subject = "Hello!";
            string message = "Message in plain text";
            var recipient = new EmailAddress("recipient-1@getproffer.net", "Recipient");

            await emailSender.SendEmailAsync(
                options.DefaultSender,
                options.DefaultSender,
                subject,
                message,
                plainTextOnly: true,
                recipient);

            fixture.Verify(
                subject: subject,
                bodyText: message,
                recipients: new() { recipient });
        }

        [Fact]
        public async Task Should_SendTemplatedEmailAsync_With_KeyAndContext()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string templateKey = "TemplatedEmail";
            var context = new
            {
                Name = "Arsène Milhaud",
                Title = "This is a templated email, Señor!",
                RawHtml = "With <b>important</b> hightlights",
                Message = "With important hightlights too, but not in bold 🙃",
            };

            var recipient = new EmailAddress("recipient-1@getproffer.net", context.Name);

            await emailSender.SendTemplatedEmailAsync(templateKey, context, recipient);

            fixture.Verify(
                subject: $"Hello {context.Name}!",
                bodyHtml: $"<h1>{WebUtility.HtmlEncode(context.Title)}</h1>{Environment.NewLine}{context.RawHtml}",
                bodyText: $"{context.Title}{Environment.NewLine}{Environment.NewLine}{context.Message}",
                recipients: new() { recipient });
        }

        [Fact]
        public async Task Should_SendTemplatedEmailAsync_With_SenderAndReplyTo()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

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

            await emailSender.SendTemplatedEmailAsync(sender, replyTo, templateKey, context, recipient);

            fixture.Verify(
                subject: $"Hello {context.Name}!",
                bodyHtml: $"<h1>{WebUtility.HtmlEncode(context.Title)}</h1>{Environment.NewLine}{context.RawHtml}",
                bodyText: $"{context.Title}{Environment.NewLine}{Environment.NewLine}{context.Message}",
                recipients: new() { recipient },
                sender: sender,
                replyTo: replyTo);
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

            await emailSender.SendEmailAsync(subject, message, recipient);

            fixture.Verify(
                subject: subject,
                bodyText: $"{message}{Environment.NewLine}{defaultDisclaimer}{Environment.NewLine}{recipient.DisplayName} ({recipient.Email})",
                bodyHtml: $"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}</body></html><br/><i>{defaultDisclaimer}<br/>{recipient.DisplayName} ({recipient.Email})</i>",
                // TODO: [https://github.com/asiffermann/proffer/issues/102] Replace with the following
                // bodyHtml: $"<!DOCTYPE html><html><head><title>{subject}</title></head><body>{message}<br/><i>{defaultDisclaimer}<br/>{recipient.DisplayName} ({recipient.Email})</i></body></html>",
                recipients: new() { new EmailAddress(mockupEmail, "Mockup Recipient") });
        }

        [Theory]
        [Bug("https://github.com/asiffermann/proffer/issues/103")]
        [InlineData("bademail.com")]
        [InlineData("joe@jim@bademail.com")]
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

            await Assert.ThrowsAsync<NotSupportedException>(() => emailSender.SendEmailAsync(subject, message, recipient));
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

            await emailSender.SendEmailAsync(subject, message, recipient);

            fixture.Verify(
                recipients: new() { recipient });
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

            await emailSender.SendEmailAsync(subject, message, firstRecipient, secondRecipient);

            fixture.Verify(
                recipients: new() { firstRecipient, new EmailAddress(mockupEmail, "Mockup Recipient") });
        }
    }
}
