namespace Providers.Templating.Handlebars
{
    using HandlebarsDotNet;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class HandlebarsTemplateProvider : ITemplateProvider
    {
        public HandlebarsTemplateProvider()
        {
            this.MimeTypes = new HashSet<string>() { "text/x-handlebars-template" };
            this.Extensions = new HashSet<string>() { ".hbs" };
        }

        public ISet<string> MimeTypes { get; }

        public ISet<string> Extensions { get; }

        public ITemplate Compile(string templateContent)
        {
            return new HandlebarsTemplate(templateContent);
        }

        public ITemplateProviderScope CreateScope()
        {
            return new Scope();
        }

        private class Scope : ITemplateProviderScope
        {
            private IHandlebars handlebars;

            public Scope()
            {
                this.handlebars = Handlebars.Create();

                this.handlebars.RegisterHelper("format", (writer, context, arguments) =>
                {
                    if (arguments.Length <= 1)
                    {
                        return;
                    }

                    var format = "";
                    if (arguments.Length > 1)
                    {
                        format = arguments[1] as string ?? "";
                    }

                    var culture = System.Globalization.CultureInfo.InvariantCulture;
                    if (arguments.Length > 2)
                    {
                        var cultureName = arguments[2] as string;
                        if (!string.IsNullOrEmpty(cultureName))
                        {
                            culture = new System.Globalization.CultureInfo(cultureName);
                        }
                    }

                    var date = arguments[0] as DateTime?;
                    if (date.HasValue)
                    {
                        writer.WriteSafeString(date.Value.ToString(format, culture));
                        return;
                    }

                    var number = arguments[0] as decimal?;
                    if (number.HasValue)
                    {
                        writer.WriteSafeString(number.Value.ToString(format, culture));
                        return;
                    }
                });
            }

            public ITemplate Compile(string templateContent)
            {
                return new HandlebarsTemplate(handlebars, templateContent);
            }

            public void RegisterPartial(string name, string template)
            {
                using (var reader = new StringReader(template))
                {
                    handlebars.RegisterTemplate(name, handlebars.Compile(reader));
                }
            }
        }
    }
}
