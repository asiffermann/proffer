namespace Proffer.Storage.Azure.Blobs
{
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Blobs.Configuration;
    using Proffer.Storage.Internal;
    using Storage;

    /// <summary>
    /// A provider to handle and build file stores pointing on an Azure Storage account.
    /// </summary>
    /// <seealso cref="StorageProviderBase{TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class AzureBlobsStorageProvider : StorageProviderBase<AzureBlobsParsedOptions, AzureBlobsProviderInstanceOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions>
    {
        /// <summary>
        /// The <see cref="AzureBlobsStorageProvider"/> name.
        /// </summary>
        public const string ProviderName = "Azure";

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobsStorageProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureBlobsStorageProvider(IOptions<AzureBlobsParsedOptions> options)
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
        protected override IStore BuildStoreInternal(string storeName, AzureBlobsStoreOptions storeOptions) => new AzureBlobsStore(storeOptions);
    }
}
