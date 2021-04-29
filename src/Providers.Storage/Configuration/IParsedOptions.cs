﻿namespace Providers.Storage.Configuration
{
    using System.Collections.Generic;

    public interface IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions>
        where TInstanceOptions : class, IProviderInstanceOptions
        where TStoreOptions : class, IStoreOptions
        where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
    {
        string Name { get; }

        IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        IReadOnlyDictionary<string, TInstanceOptions> ParsedProviderInstances { get; set; }

        IReadOnlyDictionary<string, TStoreOptions> ParsedStores { get; set; }

        IReadOnlyDictionary<string, TScopedStoreOptions> ParsedScopedStores { get; set; }

        void BindProviderInstanceOptions(TInstanceOptions providerInstanceOptions);

        void BindStoreOptions(TStoreOptions storeOptions, TInstanceOptions providerInstanceOptions = null);
    }
}
