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
    [Feature(nameof(IEmailSender.SendTemplatedEmailAsync))]
    [Collection(nameof(SendGridTestCollection))]
    public class SendTemplatedEmailTests
    {
        private readonly SendGridFixture storeFixture;
        private readonly IEmailSender emailSender;

        public SendTemplatedEmailTests(SendGridFixture fixture)
        {
            this.storeFixture = fixture;
            this.emailSender = this.storeFixture.Services.GetRequiredService<IEmailSender>();
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_SimpleArguments()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                "Notification1",
                new { },
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.FirstRecipient
                });
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_CarbonCopyRecipients()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                this.storeFixture.DefaultSender,
                "Notification1",
                new { },
                Enumerable.Empty<IEmailAttachment>(),
                new EmailAddress
                {
                    DisplayName = "recipient user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield(),
                new EmailAddress
                {
                    DisplayName = "cc user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield(),
                Array.Empty<IEmailAddress>());
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_BlackCarbonCopyRecipients()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                this.storeFixture.DefaultSender,
                "Notification1",
                new { },
                Enumerable.Empty<IEmailAttachment>(),
                new EmailAddress
                {
                    DisplayName = "recipient user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield(),
                Array.Empty<IEmailAddress>(),
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_Attachments()
        {
            byte[] data = System.IO.File.ReadAllBytes(@"Stores/Attachments/beach.jpeg");
            var image = new EmailAttachment("Beach.jpeg", data, "image", "jpeg");

            data = System.IO.File.ReadAllBytes(@"Stores/Attachments/sample.pdf");
            var pdf = new EmailAttachment("Sample.pdf", data, "application", "pdf");

            await this.emailSender.SendTemplatedEmailAsync(
                this.storeFixture.DefaultSender,
                "Notification1",
                new { },
                new List<IEmailAttachment> { image, pdf },
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.FirstRecipient
                });
        }
    }
}
