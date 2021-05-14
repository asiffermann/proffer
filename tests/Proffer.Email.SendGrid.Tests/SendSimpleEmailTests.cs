namespace Proffer.Email.SendGrid.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Email.Internal;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Email))]
    [Feature(nameof(SendGrid))]
    [Feature(nameof(IEmailSender.SendEmailAsync))]
    [Collection(nameof(SendGridTestCollection))]
    public class SendSimpleEmailTests
    {
        private readonly SendGridFixture storeFixture;
        private readonly IEmailSender emailSender;

        public SendSimpleEmailTests(SendGridFixture fixture)
        {
            this.storeFixture = fixture;
            this.emailSender = this.storeFixture.Services.GetRequiredService<IEmailSender>();
        }

        [Fact]
        public async Task Should_SendEmail_With_SimpleArguments()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new List<EmailAddress>()
                {
                    new EmailAddress
                    {
                        DisplayName = "test user",
                        Email = "tests@getproffer.net"
                    }
                })
                .AddSubject("Simple mail")
                .AddBodyText("Hello, it's a simple mail");
            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmail_With_ReplyTo()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new List<EmailAddress>()
                {
                    new EmailAddress
                    {
                        DisplayName = "test user",
                        Email = "tests@getproffer.net"
                    }
                })
                .AddSubject("SendEmail with reply")
                .AddBodyText("Hello, it's a email with reply")
                .AddReplyTo(new EmailAddress
                {
                    DisplayName = "Reply Address",
                    Email = "tests@getproffer.net"
                });

            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmail_With_CarbonCopyRecipients()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new List<EmailAddress>()
                {
                    new EmailAddress
                    {
                        DisplayName = "test user",
                        Email = "tests@getproffer.net"
                    }
                })
                .AddSubject("Cc test")
                .AddBodyText("Hello, it's a cc test")
                .AddCarbonCopyRecipient(new EmailAddress
                {
                    DisplayName = "cc user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());

            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmail_With_BlackCarbonCopyRecipients()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new List<EmailAddress>()
                {
                    new EmailAddress
                    {
                        DisplayName = "test user",
                        Email = "tests@getproffer.net"
                    }
                })
                .AddSubject("Bcc test")
                .AddBodyText("Hello, it's bcc test")
                .AddBlackCarbonCopyRecipient(new EmailAddress
                {
                    DisplayName = "bcc user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());

            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_SendEmail_With_Attachments()
        {
            byte[] data = System.IO.File.ReadAllBytes(@"Stores/Attachments/beach.jpeg");
            var image = new EmailAttachment("Beach.jpeg", data, "image", "jpeg");

            data = System.IO.File.ReadAllBytes(@"Stores/Attachments/sample.pdf");
            var pdf = new EmailAttachment("Sample.pdf", data, "application", "pdf");

            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new List<EmailAddress>()
                {
                    new EmailAddress
                    {
                        DisplayName = "test user",
                        Email = "tests@getproffer.net"
                    }
                })
                .AddSubject("Attachment test")
                .AddBodyText("Hello, it's attachment test")
                .AddAttachment(new List<IEmailAttachment> { image, pdf });

            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }

        [Fact]
        public async Task Should_Throw_With_CarbonCopyRecipientsDuplicates()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(
                this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "recipient user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield())
                .AddSubject("Cc test")
                .AddBodyText("Hello, this is an email with cc recipients")
                .AddCarbonCopyRecipient(new EmailAddress
                {
                    DisplayName = "cc user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());

            await this.emailSender.SendEmailAsync(emailBuilder.Build());
        }
    }
}
