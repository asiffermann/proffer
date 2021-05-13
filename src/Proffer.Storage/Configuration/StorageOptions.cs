namespace Proffer.Storage.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Proffer.Configuration;

    /// <summary>
    /// The Proffer.Storage options with providers and stores.
    /// </summary>
    /// <seealso cref="IParsedOptions{TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class StorageOptions : IParsedOptions<ProviderOptions, StoreOptions, ScopedStoreOptions>
    {
        /// <summary>
        /// The default configuration section name.
        /// </summary>
        public const string DefaultConfigurationSectionName = "Storage";

        private readonly Lazy<IReadOnlyDictionary<string, ProviderOptions>> parsedProviders;
        private readonly Lazy<IReadOnlyDictionary<string, StoreOptions>> parsedStores;
        private readonly Lazy<IReadOnlyDictionary<string, ScopedStoreOptions>> parsedScopedStores;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageOptions"/> class.
        /// </summary>
        public StorageOptions()
        {
            this.parsedProviders = new Lazy<IReadOnlyDictionary<string, ProviderOptions>>(
                () => this.Providers.Parse<ProviderOptions>());
            this.parsedStores = new Lazy<IReadOnlyDictionary<string, StoreOptions>>(
                () => this.Stores.Parse<StoreOptions>());
            this.parsedScopedStores = new Lazy<IReadOnlyDictionary<string, ScopedStoreOptions>>(
                () => this.ScopedStores.Parse<ScopedStoreOptions>());
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => DefaultConfigurationSectionName;

        /// <summary>
        /// Gets or sets the providers unparsed options.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationSection> Providers { get; set; }

        /// <summary>
        /// Gets or sets the stores unparsed options.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationSection> Stores { get; set; }

        /// <summary>
        /// Gets or sets the scoped stores unparsed options.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationSection> ScopedStores { get; set; }

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider instances options.
        /// </summary>
        public IReadOnlyDictionary<string, ProviderOptions> ParsedProviders { get => this.parsedProviders.Value; set { } }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        public IReadOnlyDictionary<string, StoreOptions> ParsedStores { get => FillFolderNameWithStoreNameIfItIsNull(this.parsedStores.Value); set { } }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        public IReadOnlyDictionary<string, ScopedStoreOptions> ParsedScopedStores { get => this.parsedScopedStores.Value; set { } }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindProviderOptions(ProviderOptions providerInstanceOptions) { }

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindStoreOptions(StoreOptions storeOptions, ProviderOptions providerInstanceOptions) { }

        /// <summary>
        /// If folder name is null filling it with store name
        /// </summary>
        /// <param name="parsedStores">The parsed stores.</param>
        private IReadOnlyDictionary<string, StoreOptions> FillFolderNameWithStoreNameIfItIsNull(IReadOnlyDictionary<string, StoreOptions> parsedStores)
        {
            foreach (var parsedStore in parsedStores)
            {
                if (string.IsNullOrEmpty(parsedStore.Value.FolderName))
                {
                    parsedStore.Value.FolderName = parsedStore.Value.Name;
                }
            }

            return parsedStores;
        }
    }
}
