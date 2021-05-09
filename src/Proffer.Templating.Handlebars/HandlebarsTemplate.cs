namespace Proffer.Templating.Handlebars
{
    using System;
    using HandlebarsDotNet;

    /// <summary>
    /// A template reference can be executed on a specific context using <see cref="HandlebarsDotNet"/>.
    /// </summary>
    /// <seealso cref="ITemplate" />
    public class HandlebarsTemplate : ITemplate
    {
        private readonly HandlebarsTemplate<object, object> compiledTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlebarsTemplate"/> class.
        /// </summary>
        /// <param name="templateContent">Content of the template.</param>
        public HandlebarsTemplate(string templateContent)
        {
            this.compiledTemplate = Handlebars.Compile(templateContent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlebarsTemplate"/> class.
        /// </summary>
        /// <param name="handlebars">The Handlebars service.</param>
        /// <param name="templateContent">Content of the template.</param>
        public HandlebarsTemplate(IHandlebars handlebars, string templateContent)
        {
            this.compiledTemplate = handlebars.Compile(templateContent);
        }

        /// <summary>
        /// Applies the specified context on the template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        public string Apply(object context)
            => this.compiledTemplate(context);

        /// <summary>
        /// Applies the specified context on the template with format provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        public string Apply(object context, IFormatProvider formatProvider)
            => this.compiledTemplate(context);
    }
}
