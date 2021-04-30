namespace Providers.Templating.Internal
{
    using Microsoft.Extensions.Caching.Memory;
    using Storage;
    using System.Collections.Generic;

    public class TemplateLoaderFactory : ITemplateLoaderFactory
    {
        private IMemoryCache memoryCache;
        private readonly IEnumerable<ITemplateProvider> providers;

        public TemplateLoaderFactory(IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache)
        {
            this.providers = providers;
            this.memoryCache = memoryCache;
        }

        public ITemplateLoader Create(IStore store)
        {
            return new TemplateLoader(store, providers, memoryCache, null);
        }

        public ITemplateLoader Create(IStore store, string scope)
        {
            return new TemplateLoader(store, providers, memoryCache, scope);
        }
    }
}
