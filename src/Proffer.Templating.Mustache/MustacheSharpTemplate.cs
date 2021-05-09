namespace Proffer.Templating.Mustache
{
    using System;
    using global::Mustache;

    /// <summary>
    /// A template reference can be executed on a specific context using <see cref="global::Mustache"/>.
    /// </summary>
    /// <seealso cref="ITemplate" />
    public class MustacheSharpTemplate : ITemplate
    {
        private readonly Generator compiledTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MustacheSharpTemplate"/> class.
        /// </summary>
        /// <param name="templateContent">Content of the template.</param>
        public MustacheSharpTemplate(string templateContent)
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
        public string Apply(object context)
            => this.compiledTemplate.Render(context);

        /// <summary>
        /// Applies the specified context on the template with format provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        public string Apply(object context, IFormatProvider formatProvider)
            => this.compiledTemplate.Render(formatProvider, context);
    }
}
