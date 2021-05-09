namespace Proffer.Templating
{
    using System;
    using System.Threading.Tasks;
    using Storage;

    /// <summary>
    /// Abstract base class to load and apply templates from a named <see cref="IStore"/>.
    /// </summary>
    public abstract class TemplateCollectionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateCollectionBase"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storageFactory">The storage factory.</param>
        /// <param name="templateLoaderFactory">The template loader factory.</param>
        public TemplateCollectionBase(string storeName, IStorageFactory storageFactory, ITemplateLoaderFactory templateLoaderFactory)
        {
            this.Store = storageFactory.GetStore(storeName);
            this.Loader = templateLoaderFactory.Create(this.Store);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateCollectionBase" /> class.
        /// </summary>
        /// <param name="loader">The template loader.</param>
        protected TemplateCollectionBase(ITemplateLoader loader)
        {
            this.Loader = loader;
        }

        /// <summary>
        /// Gets the templates loader.
        /// </summary>
        public ITemplateLoader Loader { get; }

        /// <summary>
        /// Gets the templates store.
        /// </summary>
        protected virtual IStore Store { get; }

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

        /// <summary>
        /// Loads the template and applies context.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        /// <param name="context">The context.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        protected async Task<string> LoadAndApplyTemplate(string templatePath, object context, IFormatProvider formatProvider)
        {
            ITemplate template = await this.Loader.GetTemplate(templatePath);
            return template.Apply(context, formatProvider);
        }
    }
}
