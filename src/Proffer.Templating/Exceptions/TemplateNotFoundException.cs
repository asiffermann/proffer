namespace Proffer.Templating.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a template was not found in the store.
    /// </summary>
    /// <seealso cref="Exception" />
    public class TemplateNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateNotFoundException" /> class.
        /// </summary>
        /// <param name="templateName">The name of the missing template.</param>
        public TemplateNotFoundException(string templateName)
            : base($"The template \"{templateName}\" was not found in the store.")
        {
        }
    }
}
