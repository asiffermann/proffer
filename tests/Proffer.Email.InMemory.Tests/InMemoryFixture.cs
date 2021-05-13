namespace Proffer.Email.InMemory.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Email.Internal;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Testing;
    using Xunit;

    public class InMemoryFixture : ServiceProviderFixtureBase
    {
        public InMemoryFixture()
        {
            IStorageFactory storageFactory = this.Services.GetRequiredService<IStorageFactory>();
            this.Attachments = storageFactory.GetStore("Attachments");
            this.Emails = this.Services.GetRequiredService<IInMemoryEmailRepository>();
        }

        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        public IInMemoryEmailRepository Emails { get; }

        public IStore Attachments { get; }

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

            bool emailsEqual(List<IEmailAddress> expected, IEnumerable<IEmailAddress> actual)
            {
                var firstNotSecond = actual.Except(expected, emailComparer).ToList();
                var secondNotFirst = expected.Except(actual, emailComparer).ToList();
                return !firstNotSecond.Any() && !secondNotFirst.Any();
            }

            bool attachmentsEqual(List<IEmailAttachment> expected, IEnumerable<IEmailAttachment> actual)
            {
                var firstNotSecond = actual.Except(expected, attachmentComparer).ToList();
                var secondNotFirst = expected.Except(actual, attachmentComparer).ToList();
                return !firstNotSecond.Any() && !secondNotFirst.Any();
            }

            bool emailWasStored = this.Emails.Store
                .Where(e => emailComparer.Equals(sender, e.From))
                .Where(e => emailsEqual(recipients, e.To))
                .Where(e => emailsEqual(ccRecipients, e.Cc))
                .Where(e => emailsEqual(bccRecipients, e.Bcc))
                .Where(e => subject == null || subject == e.Subject)
                .Where(e => bodyText == null || bodyText == e.MessageText)
                .Where(e => bodyHtml == null || bodyHtml == e.MessageHtml)
                .Where(e => attachmentsEqual(attachments, e.Attachments))
                .Where(e => replyTo == null || emailComparer.Equals(replyTo, e.ReplyTo))
                .Any();

            Assert.True(emailWasStored);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddFileSystemExtendedProperties()
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration)
                .AddInMemoryEmail();
        }
    }
}
