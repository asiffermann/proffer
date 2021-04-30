namespace Providers.Templating.Handlebars
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using HandlebarsDotNet;

    /// <summary>
    /// A provider handles and compiles templates using <see cref="HandlebarsDotNet"/>.
    /// </summary>
    /// <seealso cref="ITemplateProvider" />
    public class HandlebarsTemplateProvider : ITemplateProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlebarsTemplateProvider"/> class.
        /// </summary>
        public HandlebarsTemplateProvider()
        {
            this.MimeTypes = new HashSet<string>() { "text/x-handlebars-template" };
            this.Extensions = new HashSet<string>() { ".hbs" };
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
        /// <returns>A compiled <see cref="ITemplate"/>.</returns>
        public ITemplate Compile(string templateContent)
            => new HandlebarsTemplate(templateContent);

        /// <summary>
        /// Creates a template provider scope.
        /// </summary>
        /// <returns>
        /// A new templating provider scope.
        /// </returns>
        public ITemplateProviderScope CreateScope() => new Scope();

        private class Scope : ITemplateProviderScope
        {
            private readonly IHandlebars handlebars;

            public Scope()
            {
                this.handlebars = Handlebars.Create();

                this.handlebars.RegisterHelper("format", (writer, context, arguments) =>
                {
                    if (arguments.Length <= 1)
                    {
                        return;
                    }

                    string format = "";
                    if (arguments.Length > 1)
                    {
                        format = arguments[1] as string ?? "";
                    }

                    CultureInfo culture = CultureInfo.InvariantCulture;
                    if (arguments.Length > 2)
                    {
                        string cultureName = arguments[2] as string;
                        if (!string.IsNullOrEmpty(cultureName))
                        {
                            culture = new CultureInfo(cultureName);
                        }
                    }

                    var date = arguments[0] as DateTime?;
                    if (date.HasValue)
                    {
                        writer.WriteSafeString(date.Value.ToString(format, culture));
                        return;
                    }

                    decimal? number = arguments[0] as decimal?;
                    if (number.HasValue)
                    {
                        writer.WriteSafeString(number.Value.ToString(format, culture));
                        return;
                    }
                });
            }

            public ITemplate Compile(string templateContent)
                => new HandlebarsTemplate(this.handlebars, templateContent);

            public void RegisterPartial(string name, string template)
            {
                using (var reader = new StringReader(template))
                {
                    this.handlebars.RegisterTemplate(name, this.handlebars.Compile(reader));
                }
            }
        }
    }
}
