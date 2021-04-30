namespace Providers.Templating
{
    using System.Threading.Tasks;

    /// <summary>
    /// Loads template references.
    /// </summary>
    public interface ITemplateLoader
    {
        /// <summary>
        /// Gets a template by name.
        /// </summary>
        /// <param name="name">The template name.</param>
        /// <returns>The matching <see cref="ITemplate"/>.</returns>
        Task<ITemplate> GetTemplate(string name);
    }
}
