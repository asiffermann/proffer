namespace Providers.Storage
{
    using Configuration;

    /// <summary>
    /// A provider handles and builds file stores pointing on a particular storage system location.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Gets the name of this provider.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Builds a store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        IStore BuildStore(string storeName);

        /// <summary>
        /// Builds a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        IStore BuildStore(string storeName, IStoreOptions storeOptions);

        /// <summary>
        /// Builds a scoped store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="args">The arguments to apply to the scoped store name format.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        /// <exception cref="Exceptions.BadScopedStoreConfiguration"></exception>
        IStore BuildScopedStore(string storeName, params object[] args);
    }
}
