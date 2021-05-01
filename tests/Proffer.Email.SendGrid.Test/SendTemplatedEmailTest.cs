namespace Proffer.Email.Integration.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Email.Internal;
    using Xunit;

    [Collection(nameof(IntegrationCollection))]
    [Trait("Operation", "SendTemplated"), Trait("Kind", "Integration")]
    public class SendTemplatedEmailTest
    {
        private readonly StoresFixture storeFixture;
        private readonly IEmailSender emailSender;

        public SendTemplatedEmailTest(StoresFixture fixture)
        {
            this.storeFixture = fixture;
            this.emailSender = this.storeFixture.Services.GetRequiredService<IEmailSender>();
        }

        [Fact(DisplayName = nameof(SendNotification1))]
        public async Task SendNotification1()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                "Notification1",
                new { },
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = Datas.FirstRecipient
                });
        }

        [Fact(DisplayName = nameof(SendNotificationWithWithCC))]
        public async Task SendNotificationWithWithCC()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                new EmailAddress
                {
                    DisplayName = "Sender user test cc",
                    Email = "no-reply@proffer-dotnet.org"
                },
                "Notification1",
                new { },
                Enumerable.Empty<IEmailAttachment>(),
                new EmailAddress
                {
                    DisplayName = "recipient user",
                    Email = Datas.FirstRecipient
                }.Yield(),
                new EmailAddress
                {
                    DisplayName = "cc user",
                    Email = Datas.SecondRecipient
                }.Yield(),
                Array.Empty<IEmailAddress>());
        }

        [Fact(DisplayName = nameof(SendNotificationWithWithBbc))]
        public async Task SendNotificationWithWithBbc()
        {
            await this.emailSender.SendTemplatedEmailAsync(
                new EmailAddress
                {
                    DisplayName = "Sender user test cc",
                    Email = "no-reply@proffer-dotnet.org"
                },
                "Notification1",
                new { },
                Enumerable.Empty<IEmailAttachment>(),
                new EmailAddress
                {
                    DisplayName = "recipient user",
                    Email = Datas.FirstRecipient
                }.Yield(),
                Array.Empty<IEmailAddress>(),
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = Datas.SecondRecipient
                }.Yield());
        }

        [Fact(DisplayName = nameof(SendNotificationWithAttachments))]
        public async Task SendNotificationWithAttachments()
        {
            byte[] data = System.IO.File.ReadAllBytes(@"Attachments/beach.jpeg");
            var image = new EmailAttachment("Beach.jpeg", data, "image", "jpeg");

            data = System.IO.File.ReadAllBytes(@"Attachments/sample.pdf");
            var pdf = new EmailAttachment("Sample.pdf", data, "application", "pdf");

            await this.emailSender.SendTemplatedEmailAsync(
                new EmailAddress
                {
                    DisplayName = "test user attachm ments",
                    Email = "no-reply@proffer-dotnet.org"
                },
                "Notification1",
                new { },
                new List<IEmailAttachment> { image, pdf },
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = Datas.FirstRecipient
                });
        }
    }
}
