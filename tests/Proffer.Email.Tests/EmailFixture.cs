namespace Proffer.Email.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Testing;

    public class EmailFixture : ServiceProviderFixtureBase
    {
        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        public Mock<IEmailProviderType> ProviderTypeMock { get; private set; }

        public Mock<IEmailProvider> ProviderMock { get; private set; }

        public void Verify(
            IEmailAddress sender = null,
            List<IEmailAddress> recipients = null,
            List<IEmailAddress> ccRecipients = null,
            List<IEmailAddress> bccRecipients = null,
            string subject = null,
            string bodyText = null,
            bool plainTextOnly = false,
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

            EmailAddressEqualityComparer emailComparer = new();
            EmailAttachmentEqualityComparer attachmentComparer = new();

            Func<List<IEmailAddress>, IEnumerable<IEmailAddress>, bool> emailsEqual =
                (expected, actual) =>
                {
                    var firstNotSecond = actual.Except(expected, emailComparer).ToList();
                    var secondNotFirst = expected.Except(actual, emailComparer).ToList();
                    return !firstNotSecond.Any() && !secondNotFirst.Any();
                };

            Func<List<IEmailAttachment>, IEnumerable<IEmailAttachment>, bool> attachmentsEqual =
                (expected, actual) =>
                {
                    var firstNotSecond = actual.Except(expected, attachmentComparer).ToList();
                    var secondNotFirst = expected.Except(actual, attachmentComparer).ToList();
                    return !firstNotSecond.Any() && !secondNotFirst.Any();
                };

            this.ProviderMock.Verify(
                p => p.SendEmailAsync(
                    It.Is(sender, emailComparer),
                    It.Is<IEnumerable<IEmailAddress>>(e => emailsEqual(recipients, e)),
                    It.Is<IEnumerable<IEmailAddress>>(e => emailsEqual(ccRecipients, e)),
                    It.Is<IEnumerable<IEmailAddress>>(e => emailsEqual(bccRecipients, e)),
                    subject ?? It.IsAny<string>(),
                    bodyText ?? It.IsAny<string>(),
                    bodyHtml == null && !plainTextOnly ? ( bodyText == null ? It.IsAny<string>() : $"<html><header></header><body>{bodyText}</body></html>" ) : bodyHtml,
                    It.Is<IEnumerable<IEmailAttachment>>(a => attachmentsEqual(attachments, a)),
                    It.Is<IEmailAddress>(e => replyTo == null || emailComparer.Equals(replyTo, e))),
                Times.Once);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration);

            this.ProviderMock = new Mock<IEmailProvider>();

            this.ProviderTypeMock = new Mock<IEmailProviderType>();

            this.ProviderTypeMock
                .SetupGet(pt => pt.Name)
                .Returns("Mock");

            this.ProviderTypeMock
                .Setup(pt => pt.BuildProvider(It.IsAny<IEmailProviderOptions>()))
                .Returns(this.ProviderMock.Object);

            services.AddSingleton(sp => this.ProviderTypeMock.Object);
        }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            inMemoryCollectionData["Email:Provider:Type"] = "Mock";
        }

        private abstract class EqualityComparerBase<T> : EqualityComparer<T>
        {
            public override bool Equals(T expected, T actual)
            {
                if (expected is null && actual is null)
                {
                    return true;
                }

                if (expected is null || actual is null)
                {
                    return false;
                }

                return this.GetEqualityComponents(expected).SequenceEqual(this.GetEqualityComponents(actual));
            }

            public override int GetHashCode(T emailAddress)
            {
                return this.GetEqualityComponents(emailAddress)
                    .Aggregate(1, (current, obj) =>
                    {
                        unchecked
                        {
                            return ( current * 23 ) + ( obj?.GetHashCode() ?? 0 );
                        }
                    });
            }

            protected abstract IEnumerable<object> GetEqualityComponents(T emailAddress);
        }

        private class EmailAddressEqualityComparer : EqualityComparerBase<IEmailAddress>
        {
            protected override IEnumerable<object> GetEqualityComponents(IEmailAddress emailAddress)
            {
                yield return emailAddress.DisplayName;
                yield return emailAddress.Email;
            }
        }

        private class EmailAttachmentEqualityComparer : EqualityComparerBase<IEmailAttachment>
        {
            protected override IEnumerable<object> GetEqualityComponents(IEmailAttachment emailAttachment)
            {
                yield return emailAttachment.FileName;
                yield return emailAttachment.Data;
                yield return emailAttachment.MediaType;
                yield return emailAttachment.MediaSubtype;
                yield return emailAttachment.ContentType;
            }
        }
    }
}
