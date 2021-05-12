namespace Proffer.Email.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;
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
        public async Task Should_SendEmailAsync_With_SubjectMessageRecipient()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();

            string subject = "Hello!";
            string message = "Message in plain text";
            var firstRecipient = new EmailAddress { Email = "recipient-1@getproffer.net" };

            await emailSender.SendEmailAsync(subject, message, firstRecipient);

            fixture.Verify(
                subject: subject,
                bodyText: message,
                recipients: new() { firstRecipient });
        }

        [Fact]
        public async Task Should_SendEmailAsync_With_PlainTextOnly()
        {
            var fixture = new EmailFixture();

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            EmailOptions options = fixture.Services.GetRequiredService<IOptions<EmailOptions>>().Value;

            string subject = "Hello!";
            string message = "Message in plain text";
            var firstRecipient = new EmailAddress { Email = "recipient-1@getproffer.net" };

            await emailSender.SendEmailAsync(
                options.DefaultSender,
                options.DefaultSender,
                subject,
                message,
                plainTextOnly: true,
                firstRecipient);

            fixture.Verify(
                subject: subject,
                bodyText: message,
                recipients: new() { firstRecipient },
                plainTextOnly: true);
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
    }
}
