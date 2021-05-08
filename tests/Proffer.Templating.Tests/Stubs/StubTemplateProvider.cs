namespace Proffer.Templating.Tests.Stubs
{
    using System.Collections.Generic;

    public class StubTemplateProvider : ITemplateProvider
    {
        public StubTemplateProvider()
        {
            this.MimeTypes = new HashSet<string>() { "text/x-stub-template" };
            this.Extensions = new HashSet<string>() { ".stub" };
        }

        public ISet<string> MimeTypes { get; }

        public ISet<string> Extensions { get; }

        public ITemplateProviderScope CreateScope() => new StubTemplateProviderScope();
    }
}
