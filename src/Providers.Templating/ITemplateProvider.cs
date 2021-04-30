namespace Providers.Templating
{
    using System.Collections.Generic;

    public interface ITemplateProvider
    {
        ITemplateProviderScope CreateScope();

        ISet<string> MimeTypes { get; }

        ISet<string> Extensions { get; }
    }
}
