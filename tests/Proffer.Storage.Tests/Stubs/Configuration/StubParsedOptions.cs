namespace Proffer.Storage.Tests.Stubs.Configuration
{
    using System.Collections.Generic;
    using Proffer.Storage.Configuration;

    public class StubParsedOptions : IParsedOptions<StubProviderInstanceOptions, StubStoreOptions, StubScopedStoreOptions>
    {
        public string Name => StubStorageProvider.ProviderName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public IReadOnlyDictionary<string, StubProviderInstanceOptions> ParsedProviderInstances { get; set; }

        public IReadOnlyDictionary<string, StubStoreOptions> ParsedStores { get; set; }

        public IReadOnlyDictionary<string, StubScopedStoreOptions> ParsedScopedStores { get; set; }

        public void BindProviderInstanceOptions(StubProviderInstanceOptions providerInstanceOptions) { }

        public void BindStoreOptions(StubStoreOptions storeOptions, StubProviderInstanceOptions providerInstanceOptions = null) { }
    }
}
