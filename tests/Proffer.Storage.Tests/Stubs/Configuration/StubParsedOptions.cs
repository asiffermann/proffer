namespace Proffer.Storage.Tests.Stubs.Configuration
{
    using System.Collections.Generic;
    using Proffer.Storage.Configuration;

    public class StubParsedOptions : IParsedOptions<StubProviderOptions, StubStoreOptions, StubScopedStoreOptions>
    {
        public string Name => StubStorageProvider.ProviderName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public IReadOnlyDictionary<string, StubProviderOptions> ParsedProviders { get; set; }

        public IReadOnlyDictionary<string, StubStoreOptions> ParsedStores { get; set; }

        public IReadOnlyDictionary<string, StubScopedStoreOptions> ParsedScopedStores { get; set; }

        public void BindProviderOptions(StubProviderOptions providerInstanceOptions) { }

        public void BindStoreOptions(StubStoreOptions storeOptions, StubProviderOptions providerInstanceOptions = null) { }
    }
}
