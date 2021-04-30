namespace Providers.Templating
{
    using System;

    /// <summary>
    /// Templating scope for a configured provider.
    /// </summary>
    public interface ITemplateProviderScope
    {
        /// <summary>
        /// Compiles the specified template content.
        /// </summary>
        /// <param name="templateContent">Content of the template.</param>
        /// <returns>A new <see cref="ITemplate"/> compiled from the content.</returns>
        ITemplate Compile(string templateContent);

        /// <summary>
        /// Registers a partial template with a name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="template">The template.</param>
        /// <exception cref="NotSupportedException"></exception>
        void RegisterPartial(string name, string template);
    }
}
