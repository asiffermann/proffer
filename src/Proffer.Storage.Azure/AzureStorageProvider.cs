namespace Proffer.Storage.Azure
{
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Configuration;
    using Proffer.Storage.Internal;
    using Storage;

    /// <summary>
    /// A provider to handle and build file stores pointing on an Azure Storage account.
    /// </summary>
    /// <seealso cref="StorageProviderBase{TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class AzureStorageProvider : StorageProviderBase<AzureParsedOptions, AzureProviderInstanceOptions, AzureStoreOptions, AzureScopedStoreOptions>
    {
        /// <summary>
        /// The <see cref="AzureStorageProvider"/> name.
        /// </summary>
        public const string ProviderName = "Azure";

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureStorageProvider(IOptions<AzureParsedOptions> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the name of this provider.
        /// </summary>
        public override string Name => ProviderName;

        /// <summary>
        /// Provider-specific build of a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        protected override IStore BuildStoreInternal(string storeName, AzureStoreOptions storeOptions) => new AzureStore(storeOptions);
    }
}
