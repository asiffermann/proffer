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
    [Trait("Operation", "SendSimpleEmail"), Trait("Kind", "Integration")]
    public class SendSimpleEmailTest
    {
        private readonly EmailServicesFixture storeFixture;
        private readonly IEmailSender emailSender;

        public SendSimpleEmailTest(EmailServicesFixture fixture)
        {
            this.storeFixture = fixture;
            this.emailSender = this.storeFixture.Services.GetRequiredService<IEmailSender>();
        }

        [Fact(DisplayName = nameof(Send))]
        public async Task Send()
        {
            await this.emailSender.SendEmailAsync("Simple mail", "Hello, it's a simple mail", new EmailAddress
            {
                DisplayName = "test user",
                Email = "tests@proffer-dotnet.org"
            });
        }

        [Fact(DisplayName = nameof(SendWithReplyTo))]
        public async Task SendWithReplyTo()
        {
            await this.emailSender.SendEmailAsync(
                this.storeFixture.DefaultSender,
                new EmailAddress
                {
                    DisplayName = "Reply Address",
                    Email = "tests@proffer-dotnet.org"
                },
                "Simple mail", "Hello, it's a simple mail", false,
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = "hello@proffer-dotnet.org"
                });
        }

        [Fact(DisplayName = nameof(SendWithCC))]
        public async Task SendWithCC()
        {
            await this.emailSender.SendEmailAsync(
                this.storeFixture.DefaultSender,
                "Cc test",
                "Hello, this is an email with cc recipients",
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

        [Fact(DisplayName = nameof(SendWithBcc))]
        public async Task SendWithBcc()
        {
            await this.emailSender.SendEmailAsync(
                this.storeFixture.DefaultSender,
                "Bcc test",
                "Hello, this is an email with bcc recipients",
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

        [Fact(DisplayName = nameof(SendWithAttachments))]
        public async Task SendWithAttachments()
        {
            byte[] data = System.IO.File.ReadAllBytes(@"Attachments\beach.jpeg");
            var image = new EmailAttachment("Beach.jpeg", data, "image", "jpeg");

            data = System.IO.File.ReadAllBytes(@"Attachments\sample.pdf");
            var pdf = new EmailAttachment("Sample.pdf", data, "application", "pdf");

            await this.emailSender.SendEmailAsync(
                this.storeFixture.DefaultSender,
                "Test mail with attachments",
                "Hello, this is an email with attachments",
                new List<IEmailAttachment> { image, pdf },
                new EmailAddress
                {
                    DisplayName = "test user",
                    Email = Datas.FirstRecipient
                });
        }

        [Fact(DisplayName = nameof(ErrorSendWithCCDuplicates))]
        public async Task ErrorSendWithCCDuplicates()
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>
                 this.emailSender.SendEmailAsync(
                     this.storeFixture.DefaultSender,
                     "Cc test",
                     "Hello, this is an email with cc recipients",
                     Enumerable.Empty<IEmailAttachment>(),
                     new EmailAddress
                     {
                         DisplayName = "recipient user",
                         Email = Datas.SecondRecipient
                     }.Yield(),
                     new EmailAddress
                     {
                         DisplayName = "cc user",
                         Email = Datas.SecondRecipient
                     }.Yield(),
                     Array.Empty<IEmailAddress>()));
        }
    }
}
