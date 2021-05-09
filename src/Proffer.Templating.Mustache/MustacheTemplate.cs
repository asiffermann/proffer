namespace Proffer.Templating.Mustache
{
    using System;
    using System.Collections.Generic;
    using global::Mustache;

    /// <summary>
    /// A template reference can be executed on a specific context using <see cref="global::Mustache"/>.
    /// </summary>
    /// <seealso cref="ITemplate" />
    public class MustacheTemplate : ITemplate
    {
        private readonly Generator compiledTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MustacheTemplate"/> class.
        /// </summary>
        /// <param name="templateContent">Content of the template.</param>
        public MustacheTemplate(string templateContent)
        {
            var compiler = new FormatCompiler();

            this.compiledTemplate = compiler.Compile(templateContent);
        }

        /// <summary>
        /// Applies the specified context on the template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        /// <exception cref="InvalidContextException"></exception>
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

        /// <summary>
        /// Applies the specified context on the template with format provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        /// <exception cref="InvalidContextException"></exception>
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
