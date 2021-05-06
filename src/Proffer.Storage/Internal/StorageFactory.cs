namespace Proffer.Storage.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Default storage factory to build <see cref="IStore"/> from configured <see cref="IStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IStorageFactory" />
    public class StorageFactory : IStorageFactory
    {
        private readonly StorageOptions options;
        private readonly IReadOnlyDictionary<string, IStorageProvider> storageProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFactory"/> class.
        /// </summary>
        /// <param name="storageProviders">The storage providers.</param>
        /// <param name="options">The options.</param>
        public StorageFactory(IEnumerable<IStorageProvider> storageProviders, IOptions<StorageOptions> options)
        {
            this.storageProviders = storageProviders.ToDictionary(sp => sp.Name, sp => sp);
            this.options = options.Value;
        }

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
        public IStore GetStore(string storeName, IStoreOptions configuration)
        {
            return this.GetProvider(configuration).BuildStore(storeName, configuration);
        }

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
        public IStore GetStore(string storeName)
        {
            return this.GetProvider(this.options.GetStoreConfiguration(storeName)).BuildStore(storeName);
        }

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
        public IStore GetScopedStore(string storeName, params object[] args)
        {
            return this.GetProvider(this.options.GetScopedStoreConfiguration(storeName)).BuildScopedStore(storeName, args);
        }

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
        public bool TryGetStore(string storeName, out IStore store)
        {
            StoreOptions configuration = this.options.GetStoreConfiguration(storeName, throwIfNotFound: false);
            if (configuration != null)
            {
                IStorageProvider provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null)
                {
                    store = provider.BuildStore(storeName);
                    return true;
                }
            }

            store = null;
            return false;
        }

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
        public bool TryGetStore(string storeName, out IStore store, string providerName)
        {
            StoreOptions configuration = this.options.GetStoreConfiguration(storeName, throwIfNotFound: false);
            if (configuration != null)
            {
                IStorageProvider provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null && provider.Name == providerName)
                {
                    store = provider.BuildStore(storeName);
                    return true;
                }
            }

            store = null;
            return false;
        }

        private IStorageProvider GetProvider(IStoreOptions configuration, bool throwIfNotFound = true)
        {
            string providerTypeName = null;
            if (!string.IsNullOrEmpty(configuration.ProviderType))
            {
                providerTypeName = configuration.ProviderType;
            }
            else if (!string.IsNullOrEmpty(configuration.ProviderName))
            {
                this.options.ParsedProviders.TryGetValue(configuration.ProviderName, out ProviderOptions providerInstanceOptions);
                if (providerInstanceOptions != null)
                {
                    providerTypeName = providerInstanceOptions.Type;
                }
                else if (throwIfNotFound)
                {
                    throw new Exceptions.BadProviderConfiguration(configuration.ProviderName, "Unable to find it in the configuration.");
                }
            }
            else if (throwIfNotFound)
            {
                throw new Exceptions.BadStoreConfiguration(configuration.Name, "You have to set either 'ProviderType' or 'ProviderName' on Store configuration.");
            }

            if (string.IsNullOrEmpty(providerTypeName))
            {
                return null;
            }

            this.storageProviders.TryGetValue(providerTypeName, out IStorageProvider provider);
            if (provider == null && throwIfNotFound)
            {
                throw new Exceptions.ProviderNotFoundException(providerTypeName);
            }

            return provider;
        }
    }
}
