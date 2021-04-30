namespace Providers.Templating.Handlebars
{
    using HandlebarsDotNet;
    using System;
    using System.Collections.Generic;

    public class HandlebarsTemplate : ITemplate
    {
        private HandlebarsTemplate<object, object> compiledTemplate;

        public HandlebarsTemplate(string templateContent)
        {
            this.compiledTemplate = Handlebars.Compile(templateContent);
        }

        public HandlebarsTemplate(IHandlebars handlebars, string templateContent)
        {
            this.compiledTemplate = handlebars.Compile(templateContent);
        }

        public string Apply(object context)
        {
            try
            {
                return this.compiledTemplate(context);
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
                return this.compiledTemplate(formatProvider);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidContextException(ex);
            }
        }
    }
}
