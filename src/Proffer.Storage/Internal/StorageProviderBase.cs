namespace Proffer.Storage.Internal
{
    using System;
    using Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A base provider to handle and build file stores pointing on a particular storage system location.
    /// </summary>
    /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
    /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
    /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
    /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
    /// <seealso cref="IStorageProvider" />
    public abstract class StorageProviderBase<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions> : IStorageProvider
        where TParsedOptions : class, IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions>, new()
        where TInstanceOptions : class, IProviderInstanceOptions, new()
        where TStoreOptions : class, IStoreOptions, new()
        where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// The parsed options with providers and stores.
        /// </summary>
        protected readonly TParsedOptions Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageProviderBase{TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions}"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public StorageProviderBase(IOptions<TParsedOptions> options)
        {
            this.Options = options.Value;
        }

        /// <summary>
        /// Gets the name of this provider.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Builds a store from configured options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        public IStore BuildStore(string storeName)
        {
            return this.BuildStoreInternal(storeName, this.Options.GetStoreConfiguration(storeName));
        }

        /// <summary>
        /// Builds a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        public IStore BuildStore(string storeName, IStoreOptions storeOptions)
        {
            return this.BuildStoreInternal(
                storeName,
                storeOptions.ParseStoreOptions<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this.Options));
        }

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
        public IStore BuildScopedStore(string storeName, params object[] args)
        {
            TScopedStoreOptions scopedStoreOptions = this.Options.GetScopedStoreConfiguration(storeName);

            try
            {
                scopedStoreOptions.FolderName = string.Format(scopedStoreOptions.FolderNameFormat, args);
            }
            catch (Exception ex)
            {
                throw new Exceptions.BadScopedStoreConfiguration(storeName, "Cannot format folder name. See InnerException for details.", ex);
            }

            return this.BuildStoreInternal(storeName, scopedStoreOptions.ParseStoreOptions<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this.Options));
        }

        /// <summary>
        /// Provider-specific build of a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        protected abstract IStore BuildStoreInternal(string storeName, TStoreOptions storeOptions);
    }
}
