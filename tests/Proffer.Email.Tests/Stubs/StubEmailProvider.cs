namespace Proffer.Email.Tests.Stubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StubEmailProvider : IEmailProvider
    {
        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml)
            => throw new NotImplementedException();

        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments)
            => throw new NotImplementedException();

        public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo = null)
            => throw new NotImplementedException();
    }
}
