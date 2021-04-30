namespace Providers.Templating.Internal
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Storage;

    /// <summary>
    /// Creates <see cref="ITemplateLoader"/> from <see cref="IStore"/>.
    /// </summary>
    /// <seealso cref="ITemplateLoaderFactory" />
    public class TemplateLoaderFactory : ITemplateLoaderFactory
    {
        private readonly IMemoryCache memoryCache;
        private readonly IEnumerable<ITemplateProvider> providers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoaderFactory"/> class.
        /// </summary>
        /// <param name="providers">The providers.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public TemplateLoaderFactory(IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache)
        {
            this.providers = providers;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Creates a template loader from the specified store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns>
        /// A <see cref="ITemplateLoader" /> that loads templates from the given <see cref="IStore" />.
        /// </returns>
        public ITemplateLoader Create(IStore store)
            => new TemplateLoader(store, this.providers, this.memoryCache, null);

        /// <summary>
        /// Creates a template loader from the specified store with the specified cache scope.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="scope">The cache scope.</param>
        /// <returns>
        /// A <see cref="ITemplateLoader" /> that loads templates from the given <see cref="IStore" />.
        /// </returns>
        public ITemplateLoader Create(IStore store, string scope)
            => new TemplateLoader(store, this.providers, this.memoryCache, scope);
    }
}
