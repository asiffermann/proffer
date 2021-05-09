namespace Proffer.Templating.Tests.Stubs
{
    using System.Collections.Generic;

    public class StubTemplateProviderScope : ITemplateProviderScope
    {
        private readonly Dictionary<string, string> partials = new();

        public ITemplate Compile(string templateContent) => new StubTemplate(templateContent);

        public void RegisterPartial(string name, string template)
            => this.partials[name] = template;
    }
}
