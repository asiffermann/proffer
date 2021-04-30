namespace Providers.Templating
{
    using Storage;
    using System.Threading.Tasks;

    public abstract class TemplateCollectionBase
    {
        public TemplateCollectionBase(string storeName, IStorageFactory storageFactory, ITemplateLoaderFactory templateLoaderFactory)
        {
            this.Loader = templateLoaderFactory.Create(storageFactory.GetStore(storeName));
        }

        public ITemplateLoader Loader { get; }

        protected async Task<string> LoadAndApplyTemplate(string templatePath, object context)
        {
            var template = await this.Loader.GetTemplate(templatePath);
            return template.Apply(context);
        }
    }

    public abstract class TemplateCollectionBase<TStore> where TStore : StoreBase
    {
        public TemplateCollectionBase(TStore store, ITemplateLoaderFactory templateLoaderFactory)
        {
            this.Loader = templateLoaderFactory.Create(store.Store);
        }

        public ITemplateLoader Loader { get; }

        protected async Task<string> LoadAndApplyTemplate(string templatePath, object context)
        {
            var template = await this.Loader.GetTemplate(templatePath);
            return template.Apply(context);
        }
    }
}
