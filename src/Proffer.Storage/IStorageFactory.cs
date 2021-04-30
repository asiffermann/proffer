namespace Proffer.Storage
{
    using Configuration;

    /// <summary>
    /// Builds <see cref="IStore"/> from configured <see cref="IStorageProvider"/>.
    /// </summary>
    public interface IStorageFactory
    {
        /// <summary>
        /// Gets a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="configuration">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFoundException"></exception>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        IStore GetStore(string storeName, IStoreOptions configuration);

        /// <summary>
        /// Gets a store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFoundException"></exception>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        IStore GetStore(string storeName);

        /// <summary>
        /// Gets a scoped store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the scoped store.</param>
        /// <param name="args">The arguments to apply to the scoped store name format.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFoundException"></exception>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        /// <exception cref="Exceptions.BadScopedStoreConfiguration"></exception>
        IStore GetScopedStore(string storeName, params object[] args);

        /// <summary>
        /// Gets a store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="store">When this method returns, contains the store associated with the specified name, if it is found in the <see cref="StorageOptions" />; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>
        ///   <c>true</c> if the store was configured and built from its provider; otherwise, false.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFoundException"></exception>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        bool TryGetStore(string storeName, out IStore store);

        /// <summary>
        /// Gets a store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="store">When this method returns, contains the store associated with the specified name, if it is found in the <see cref="StorageOptions" />; otherwise, null. This parameter is passed uninitialized.</param>
        /// <param name="providerName">The explicit provider name from which the store should be built.</param>
        /// <returns>
        ///   <c>true</c> if the store was configured and built from its provider; otherwise, false.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFoundException"></exception>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        bool TryGetStore(string storeName, out IStore store, string providerName);
    }
}
