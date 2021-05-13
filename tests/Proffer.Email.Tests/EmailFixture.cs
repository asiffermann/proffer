namespace Proffer.Email.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;
    using Proffer.Email.Internal;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Testing;

    public class EmailFixture : ServiceProviderFixtureBase
    {
        private readonly IDictionary<string, string> inMemoryConfiguration;

        public EmailFixture()
        {
            this.Init();
        }

        public EmailFixture(Dictionary<string, string> inMemoryConfiguration = null)
            : base(false)
        {
            this.inMemoryConfiguration = inMemoryConfiguration;
            this.Build();
            this.Init();
        }

        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        public Mock<IEmailProviderType> ProviderTypeMock { get; private set; }

        public Mock<IEmailProvider> ProviderMock { get; private set; }

        public IStore Attachments { get; private set; }

        public void Verify(
            IEmailAddress sender = null,
            List<IEmailAddress> recipients = null,
            List<IEmailAddress> ccRecipients = null,
            List<IEmailAddress> bccRecipients = null,
            string subject = null,
            string bodyText = null,
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

            EmailAddressStrictEqualityComparer emailComparer = new();
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
                    It.Is<string>(s => subject == null || subject == s),
                    It.Is<string>(bt => bodyText == null || bodyText == bt),
                    It.Is<string>(bh => bodyHtml == null || bodyHtml == bh),
                    It.Is<IEnumerable<IEmailAttachment>>(a => attachmentsEqual(attachments, a)),
                    It.Is<IEmailAddress>(e => replyTo == null || emailComparer.Equals(replyTo, e))),
                Times.Once);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddFileSystemExtendedProperties()
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

            if (this.inMemoryConfiguration == null)
            {
                return;
            }

            foreach (KeyValuePair<string, string> kvp in this.inMemoryConfiguration)
            {
                inMemoryCollectionData.Add(kvp.Key, kvp.Value);
            }
        }

        private void Init()
        {
            IStorageFactory storageFactory = this.Services.GetRequiredService<IStorageFactory>();
            this.Attachments = storageFactory.GetStore("Attachments");
        }
    }
}
