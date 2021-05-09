namespace Proffer.Templating.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a matching provider was not found for a given template.
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProviderNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderNotFoundException" /> class.
        /// </summary>
        /// <param name="templateName">The name of the unmatched template.</param>
        /// <param name="extension">The extension of the unmatched template.</param>
        /// <param name="contentType">The MIME type of the unmatched template.</param>
        public ProviderNotFoundException(string templateName, string extension, string contentType)
            : base($"No provider was found for the template \"{templateName}\" (Extension: {extension}, Content-Type: {contentType}). Did you forget to register providers in your Startup.ConfigureServices?")
        {
        }
    }
}
