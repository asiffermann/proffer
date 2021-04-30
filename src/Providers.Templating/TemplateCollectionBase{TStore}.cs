namespace Providers.Templating
{
    using System.Threading.Tasks;
    using Storage;

    /// <summary>
    /// Abstract base class to load and apply templates from a typed <typeparamref name="TStore"/>.
    /// </summary>
    /// <typeparam name="TStore">The type of the store.</typeparam>
    public abstract class TemplateCollectionBase<TStore>
        where TStore : StoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateCollectionBase{TStore}"/> class.
        /// </summary>
        /// <param name="store">The typed store.</param>
        /// <param name="templateLoaderFactory">The template loader factory.</param>
        public TemplateCollectionBase(TStore store, ITemplateLoaderFactory templateLoaderFactory)
        {
            this.Loader = templateLoaderFactory.Create(store.Store);
        }

        /// <summary>
        /// Gets the loader.
        /// </summary>
        public ITemplateLoader Loader { get; }

        /// <summary>
        /// Loads the template and applies context.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        /// <param name="context">The context.</param>
        /// <returns>The templated result.</returns>
        protected async Task<string> LoadAndApplyTemplate(string templatePath, object context)
        {
            ITemplate template = await this.Loader.GetTemplate(templatePath);
            return template.Apply(context);
        }
    }
}
