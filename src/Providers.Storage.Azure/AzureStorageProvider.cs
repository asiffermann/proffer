namespace Providers.Storage.Azure
{
    using Providers.Storage.Azure.Configuration;
    using Providers.Storage.Internal;
    using Microsoft.Extensions.Options;
    using Storage;

    public class AzureStorageProvider : StorageProviderBase<AzureParsedOptions, AzureProviderInstanceOptions, AzureStoreOptions, AzureScopedStoreOptions>
    {
        public const string ProviderName = "Azure";

        public AzureStorageProvider(IOptions<AzureParsedOptions> options)
            : base(options)
        {
        }

        public override string Name => ProviderName;

        protected override IStore BuildStoreInternal(string storeName, AzureStoreOptions storeOptions)
        {
            return new AzureStore(storeOptions);
        }
    }
}
