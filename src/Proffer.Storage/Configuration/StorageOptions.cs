namespace Proffer.Storage.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The Proffer.Storage options with providers and stores.
    /// </summary>
    /// <seealso cref="IParsedOptions{TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class StorageOptions : IParsedOptions<ProviderInstanceOptions, StoreOptions, ScopedStoreOptions>
    {
        /// <summary>
        /// The default configuration section name.
        /// </summary>
        public const string DefaultConfigurationSectionName = "Storage";

        private readonly Lazy<IReadOnlyDictionary<string, ProviderInstanceOptions>> parsedProviderInstances;
        private readonly Lazy<IReadOnlyDictionary<string, StoreOptions>> parsedStores;
        private readonly Lazy<IReadOnlyDictionary<string, ScopedStoreOptions>> parsedScopedStores;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageOptions"/> class.
        /// </summary>
        public StorageOptions()
        {
            this.parsedProviderInstances = new Lazy<IReadOnlyDictionary<string, ProviderInstanceOptions>>(
                () => this.Proffer.Parse<ProviderInstanceOptions>());
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
        public IReadOnlyDictionary<string, IConfigurationSection> Proffer { get; set; }

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
        public IReadOnlyDictionary<string, ProviderInstanceOptions> ParsedProviderInstances { get => this.parsedProviderInstances.Value; set { } }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        public IReadOnlyDictionary<string, StoreOptions> ParsedStores { get => this.parsedStores.Value; set { } }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        public IReadOnlyDictionary<string, ScopedStoreOptions> ParsedScopedStores { get => this.parsedScopedStores.Value; set { } }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindProviderInstanceOptions(ProviderInstanceOptions providerInstanceOptions)
        {
        }

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindStoreOptions(StoreOptions storeOptions, ProviderInstanceOptions providerInstanceOptions)
        {
        }
    }
}
