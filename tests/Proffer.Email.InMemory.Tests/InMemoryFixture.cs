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

        public void Verify(IEmail email)
        {
            IOptions<EmailOptions> options = this.Services.GetRequiredService<IOptions<EmailOptions>>();

            email.From ??= options.Value.DefaultSender;
            email.Recipients ??= new List<IEmailAddress>();
            email.CcRecipients ??= new List<IEmailAddress>();
            email.BccRecipients ??= new List<IEmailAddress>();
            email.Attachments ??= new List<IEmailAttachment>();

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
                .Where(e => emailComparer.Equals(email.From, e.From))
                .Where(e => emailsEqual(email.Recipients.ToList(), e.To))
                .Where(e => emailsEqual(email.CcRecipients.ToList(), e.Cc))
                .Where(e => emailsEqual(email.BccRecipients.ToList(), e.Bcc))
                .Where(e => email.Subject == null || email.Subject == e.Subject)
                .Where(e => email.BodyText == null || email.BodyText == e.MessageText)
                .Where(e => email.BodyHtml == null || email.BodyHtml == e.MessageHtml)
                .Where(e => attachmentsEqual(email.Attachments.ToList(), e.Attachments))
                .Where(e => email.ReplyTo == null || emailComparer.Equals(email.ReplyTo, e.ReplyTo))
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
