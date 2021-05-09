namespace Proffer.Templating
{
    using Storage;

    /// <summary>
    /// Abstract base class to load and apply templates from a typed <typeparamref name="TStore"/>.
    /// </summary>
    /// <typeparam name="TStore">The type of the store.</typeparam>
    public abstract class TemplateCollectionBase<TStore> : TemplateCollectionBase
        where TStore : StoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateCollectionBase{TStore}"/> class.
        /// </summary>
        /// <param name="store">The typed store.</param>
        /// <param name="templateLoaderFactory">The template loader factory.</param>
        public TemplateCollectionBase(TStore store, ITemplateLoaderFactory templateLoaderFactory)
            : base(templateLoaderFactory.Create(store.Store))
        {
            this.Store = store;
        }

        /// <summary>
        /// Gets the templates store.
        /// </summary>
        protected new TStore Store { get; }
    }
}
