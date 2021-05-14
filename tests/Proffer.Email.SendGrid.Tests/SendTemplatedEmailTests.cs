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
            IEmailBuilder emailBuilder = new EmailBuilder(this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield())
                .AddSubject("Notification1");

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), new { });
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_CarbonCopyRecipients()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "recipent user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield())
                .AddSubject("Notification1")
                .AddCarbonCopyRecipient(new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), new { });
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_BlackCarbonCopyRecipients()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "recipent user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield())
                .AddSubject("Notification1")
                .AddBlackCarbonCopyRecipient(new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.SecondRecipient
                }.Yield());

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), new { });
        }

        [Fact]
        public async Task Should_SendTemplatedEmail_With_Attachments()
        {
            byte[] data = System.IO.File.ReadAllBytes(@"Stores/Attachments/beach.jpeg");
            var image = new EmailAttachment("Beach.jpeg", data, "image", "jpeg");

            data = System.IO.File.ReadAllBytes(@"Stores/Attachments/sample.pdf");
            var pdf = new EmailAttachment("Sample.pdf", data, "application", "pdf");

            IEmailBuilder emailBuilder = new EmailBuilder(this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = SendGridFixture.FirstRecipient
                }.Yield())
                .AddSubject("Notification1")
                .AddAttachment(new List<IEmailAttachment> { image, pdf });

            await this.emailSender.SendTemplatedEmailAsync(emailBuilder.Build(), new { });
        }
    }
}
