namespace Providers.Templating.Internal
{
    using Microsoft.Extensions.Caching.Memory;
    using Storage;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TemplateLoader : ITemplateLoader
    {
        private IMemoryCache memoryCache;
        private IEnumerable<ITemplateProvider> providers;
        private IStore store;
        //private Dictionary<ITemplateProvider, ITemplateScope> scopes;

        public TemplateLoader(IStore store, IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache, string scope)
        {
            this.store = store;
            this.providers = providers;
            this.memoryCache = memoryCache;
            //this.scope = memoryCache.GetOrCreateAsync("x_tmpl_inf_scopes", async entry => { })
        }

        public Task<ITemplate> GetTemplate(string name)
        {
            return this.memoryCache.GetOrCreateAsync($"x_tmpl_{this.store.Name}_{name}", async entry =>
            {
                string directory;
                string file;
                var lastSlash = name.LastIndexOf('/');
                if (lastSlash < 0)
                {
                    directory = "";
                    file = name;
                }
                else
                {
                    directory = name.Substring(0, lastSlash);
                    file = name.Substring(lastSlash + 1);
                }

                entry.SetPriority(CacheItemPriority.High);
                var fileReference = (await this.store.ListAsync(directory, $"{file}.*")).First();
                var provider = this.providers.First(x => x.Extensions.Any(ext => fileReference.Path.EndsWith(ext)));

                var scope = await GetScope(provider, directory);

                return scope.Compile(await this.store.ReadAllTextAsync(fileReference));
            });
        }

        private Task<ITemplateProviderScope> GetScope(ITemplateProvider provider, string name)
        {
            return this.memoryCache.GetOrCreateAsync($"x_tmpl_{this.store.Name}_inf_scopes_{provider.Extensions.First()}_{name}", async entry =>
            {
                entry.SetPriority(CacheItemPriority.High);
                var scope = provider.CreateScope();
                var files = await this.store.ListAsync(name, "_*.*");
                foreach (var file in files)
                {
                    var path = file.Path.Split('/');
                    var fileName = path.Last();
                    if (fileName.StartsWith("_") && provider.Extensions.Any(ext => fileName.EndsWith(ext)))
                    {
                        var partialName = System.IO.Path.GetFileNameWithoutExtension(fileName.Substring(1));
                        scope.RegisterPartial(partialName, await this.store.ReadAllTextAsync(file));
                    }
                }

                return scope;
            });
        }
    }
}
