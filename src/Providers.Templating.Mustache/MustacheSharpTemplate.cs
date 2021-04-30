namespace Providers.Templating.Mustache
{
    using global::Mustache;
    using System;
    using System.Collections.Generic;

    public class MustacheSharpTemplate : ITemplate
    {
        private Generator compiledTemplate;

        public MustacheSharpTemplate(string templateContent)
        {
            FormatCompiler compiler = new FormatCompiler();

            this.compiledTemplate = compiler.Compile(templateContent);
        }

        public string Apply(object context)
        {
            try
            {
                return this.compiledTemplate.Render(context);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidContextException(ex);
            }
        }

        public string Apply(object context, IFormatProvider formatProvider)
        {
            try
            {
                return this.compiledTemplate.Render(formatProvider, context);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidContextException(ex);
            }
        }
    }
}
