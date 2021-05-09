namespace Proffer.Templating.Internal
{
    using System.Collections.Generic;
    using Dawn;
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
        {
            Guard.Argument(store, nameof(store)).NotNull();
            return new TemplateLoader(store, this.providers, this.memoryCache);
        }

        /// <summary>
        /// Creates a template loader from the specified store with the specified cache scope.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>
        /// A <see cref="ITemplateLoader" /> that loads templates from the given <see cref="IStore" />.
        /// </returns>
        /// <remarks>Don't know what was the purpose of this override...</remarks>
        public ITemplateLoader Create(IStore store, string scope)
        {
            Guard.Argument(store, nameof(store)).NotNull();
            Guard.Argument(scope, nameof(scope)).NotNull().NotEmpty();
            return new TemplateLoader(store, this.providers, this.memoryCache);
        }
    }
}
