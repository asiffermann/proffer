namespace Proffer.Storage.Azure.Blobs.Configuration
{
    using System.Collections.Generic;
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Typed Azure Blobs options parsed from the dynamic configuration.
    /// </summary>
    /// <seealso cref="IParsedOptions{TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class AzureBlobsParsedOptions : IParsedOptions<AzureBlobsProviderOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => AzureBlobsStorageProvider.ProviderName;

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider instances options.
        /// </summary>
        public IReadOnlyDictionary<string, AzureBlobsProviderOptions> ParsedProviders { get; set; }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        public IReadOnlyDictionary<string, AzureBlobsStoreOptions> ParsedStores { get; set; }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        public IReadOnlyDictionary<string, AzureBlobsScopedStoreOptions> ParsedScopedStores { get; set; }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        public void BindProviderOptions(AzureBlobsProviderOptions providerInstanceOptions)
        {
            if (!string.IsNullOrEmpty(providerInstanceOptions.ConnectionStringName)
                && string.IsNullOrEmpty(providerInstanceOptions.ConnectionString))
            {
                if (!this.ConnectionStrings.ContainsKey(providerInstanceOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadProviderConfiguration(
                        providerInstanceOptions.Name,
                        $"The ConnectionString '{providerInstanceOptions.ConnectionStringName}' cannot be found. Did you call AddStorage with the ConfigurationRoot?");
                }

                providerInstanceOptions.ConnectionString = this.ConnectionStrings[providerInstanceOptions.ConnectionStringName];
            }
        }

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        public void BindStoreOptions(AzureBlobsStoreOptions storeOptions, AzureBlobsProviderOptions providerInstanceOptions = null)
        {
            storeOptions.FolderName = storeOptions.FolderName.ToLowerInvariant();

            if (!string.IsNullOrEmpty(storeOptions.ConnectionStringName)
                && string.IsNullOrEmpty(storeOptions.ConnectionString))
            {
                if (!this.ConnectionStrings.ContainsKey(storeOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadStoreConfiguration(
                        storeOptions.Name,
                        $"The ConnectionString '{storeOptions.ConnectionStringName}' cannot be found. Did you call AddStorage with the ConfigurationRoot?");
                }

                storeOptions.ConnectionString = this.ConnectionStrings[storeOptions.ConnectionStringName];
            }

            if (providerInstanceOptions == null
                || storeOptions.ProviderName != providerInstanceOptions.Name)
            {
                return;
            }

            if (string.IsNullOrEmpty(storeOptions.ConnectionString))
            {
                storeOptions.ConnectionString = providerInstanceOptions.ConnectionString;
            }
        }
    }
}
