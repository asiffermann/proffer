namespace Proffer.Templating.Mustache
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A provider handles and compiles templates using <see cref="global::Mustache"/>.
    /// </summary>
    /// <seealso cref="ITemplateProvider" />
    public class MustacheTemplateProvider : ITemplateProvider, ITemplateProviderScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MustacheTemplateProvider"/> class.
        /// </summary>
        public MustacheTemplateProvider()
        {
            this.MimeTypes = new HashSet<string>() { "text/x-mustache-template" };
            this.Extensions = new HashSet<string>() { ".mustache" };
        }

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        public ISet<string> MimeTypes { get; }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        public ISet<string> Extensions { get; }

        /// <summary>
        /// Compiles the specified template content.
        /// </summary>
        /// <param name="templateContent">Content of the template.</param>
        /// <returns>
        /// A new <see cref="ITemplate" /> compiled from the content.
        /// </returns>
        public ITemplate Compile(string templateContent)
            => new MustacheTemplate(templateContent);

        /// <summary>
        /// Creates a template provider scope.
        /// </summary>
        /// <returns>
        /// A new templating provider scope.
        /// </returns>
        public ITemplateProviderScope CreateScope()
            => this;

        /// <summary>
        /// Registers a partial template with a name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="template">The template.</param>
        /// <exception cref="NotSupportedException"></exception>
        public void RegisterPartial(string name, string template)
            => throw new NotSupportedException();
    }
}
