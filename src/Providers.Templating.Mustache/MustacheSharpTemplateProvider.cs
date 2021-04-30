namespace Providers.Templating.Mustache
{
    using System;
    using System.Collections.Generic;

    public class MustacheSharpTemplateProvider : ITemplateProvider, ITemplateProviderScope
    {
        public MustacheSharpTemplateProvider()
        {
            this.MimeTypes = new HashSet<string>() { "text/x-mustache-template" };
            this.Extensions = new HashSet<string>() { ".mustache" };
        }

        public ISet<string> MimeTypes { get; }

        public ISet<string> Extensions { get; }

        public ITemplate Compile(string templateContent)
        {
            return new MustacheSharpTemplate(templateContent);
        }

        public ITemplateProviderScope CreateScope()
        {
            return this;
        }

        public void RegisterPartial(string name, string template)
        {
            throw new NotSupportedException();
        }
    }
}
