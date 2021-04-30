namespace Providers.Templating
{
    using Storage;

    /// <summary>
    /// Creates <see cref="ITemplateLoader"/> from <see cref="IStore"/>.
    /// </summary>
    public interface ITemplateLoaderFactory
    {
        /// <summary>
        /// Creates a template loader from the specified store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns>A <see cref="ITemplateLoader"/> that loads templates from the given <see cref="IStore"/>.</returns>
        ITemplateLoader Create(IStore store);

        /// <summary>
        /// Creates a template loader from the specified store with the specified cache scope.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="scope">The cache scope.</param>
        /// <returns>A <see cref="ITemplateLoader"/> that loads templates from the given <see cref="IStore"/>.</returns>
        ITemplateLoader Create(IStore store, string scope);
    }
}
