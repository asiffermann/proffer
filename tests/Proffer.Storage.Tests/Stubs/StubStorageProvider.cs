namespace Proffer.Storage.Tests.Stubs
{
    using Microsoft.Extensions.Options;
    using Proffer.Storage;
    using Proffer.Storage.Internal;
    using Proffer.Storage.Tests.Stubs.Configuration;

    public class StubStorageProvider : StorageProviderBase<StubParsedOptions, StubProviderInstanceOptions, StubStoreOptions, StubScopedStoreOptions>
    {
        public const string ProviderName = "Stub";

        public StubStorageProvider(IOptions<StubParsedOptions> options)
            : base(options)
        {
        }

        public override string Name => ProviderName;

        protected override IStore BuildStoreInternal(string storeName, StubStoreOptions storeOptions) => new StubStore(storeOptions);
    }
}
