namespace Proffer.Templating
{
    using System.Collections.Generic;

    /// <summary>
    /// A provider handles and compiles templates using a particular templating library.
    /// </summary>
    public interface ITemplateProvider
    {
        /// <summary>
        /// Creates a template provider scope.
        /// </summary>
        /// <returns>A new templating provider scope.</returns>
        ITemplateProviderScope CreateScope();

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        ISet<string> MimeTypes { get; }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        ISet<string> Extensions { get; }
    }
}
