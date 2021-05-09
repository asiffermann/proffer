namespace Proffer.Templating.Tests.Stubs
{
    using System;

    public class StubTemplate : ITemplate
    {
        private readonly string templateContent;

        public StubTemplate(string templateContent)
        {
            this.templateContent = templateContent;
        }

        public string Apply(object context) => string.Format(this.templateContent, context);

        public string Apply(object context, IFormatProvider formatProvider) => string.Format(formatProvider, this.templateContent, context);
    }
}
