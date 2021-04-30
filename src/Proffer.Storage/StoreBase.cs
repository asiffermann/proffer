namespace Proffer.Storage
{
    /// <summary>
    /// Abstract base typed store.
    /// </summary>
    public abstract class StoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreBase"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storageFactory">The storage factory.</param>
        public StoreBase(string storeName, IStorageFactory storageFactory)
        {
            this.Store = storageFactory.GetStore(storeName);
        }

        /// <summary>
        /// Gets the store.
        /// </summary>
        public IStore Store { get; }
    }
}
