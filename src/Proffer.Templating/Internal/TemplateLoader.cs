namespace Proffer.Templating.Internal
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Proffer.Templating.Exceptions;
    using Storage;

    /// <summary>
    /// Loads template references from an <see cref="IStore"/> and cache results.
    /// </summary>
    public class TemplateLoader : ITemplateLoader
    {
        private readonly IMemoryCache memoryCache;
        private readonly IEnumerable<ITemplateProvider> providers;
        private readonly IStore store;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoader"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="providers">The providers.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public TemplateLoader(IStore store, IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache)
        {
            this.store = store;
            this.providers = providers;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets a template by name.
        /// </summary>
        /// <param name="name">The template name.</param>
        /// <returns>
        /// The matching <see cref="ITemplate" />.
        /// </returns>
        public Task<ITemplate> GetTemplate(string name)
        {
            return this.memoryCache.GetOrCreateAsync($"x_tmpl_{this.store.Name}_{name}", async entry =>
            {
                string directory;
                string file;
                int lastSlash = name.LastIndexOf('/');
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

                IFileReference[] files = await this.store.ListAsync(directory, $"{file}.*", withMetadata: true);
                if (files == null || files.Length == 0)
                {
                    throw new TemplateNotFoundException(name);
                }

                IFileReference fileReference = files.First();

                ITemplateProvider provider = this.providers
                    .FirstOrDefault(p => p.Extensions.Any(ext => fileReference.Path.EndsWith(ext))
                        || p.MimeTypes.Any(type => fileReference.Properties.ContentType == type));

                if (provider == null)
                {
                    throw new ProviderNotFoundException(name, Path.GetExtension(fileReference.Path), fileReference.Properties.ContentType);
                }

                ITemplateProviderScope scope = await this.GetScope(provider, directory);

                return scope.Compile(await this.store.ReadAllTextAsync(fileReference));
            });
        }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The scope name.</param>
        /// <returns></returns>
        private Task<ITemplateProviderScope> GetScope(ITemplateProvider provider, string name)
        {
            return this.memoryCache.GetOrCreateAsync($"x_tmpl_{this.store.Name}_inf_scopes_{provider.Extensions.First()}_{name}", async entry =>
            {
                entry.SetPriority(CacheItemPriority.High);
                ITemplateProviderScope scope = provider.CreateScope();
                IFileReference[] files = await this.store.ListAsync(name, "_*.*");
                foreach (IFileReference file in files)
                {
                    string[] path = file.Path.Split('/');
                    string fileName = path.Last();
                    if (fileName.StartsWith("_") && provider.Extensions.Any(ext => fileName.EndsWith(ext)))
                    {
                        string partialName = Path.GetFileNameWithoutExtension(fileName.Substring(1));
                        scope.RegisterPartial(partialName, await this.store.ReadAllTextAsync(file));
                    }
                }

                return scope;
            });
        }
    }
}
