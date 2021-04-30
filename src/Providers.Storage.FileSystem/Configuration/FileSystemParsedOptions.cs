namespace Providers.Storage.FileSystem.Configuration
{
    using System.Collections.Generic;
    using System.IO;
    using Providers.Storage.Configuration;

    /// <summary>
    /// Typed File System options parsed from the dynamic configuration.
    /// </summary>
    /// <seealso cref="IParsedOptions{TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class FileSystemParsedOptions : IParsedOptions<FileSystemProviderInstanceOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => FileSystemStorageProvider.ProviderName;

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider instances options.
        /// </summary>
        public IReadOnlyDictionary<string, FileSystemProviderInstanceOptions> ParsedProviderInstances { get; set; }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        public IReadOnlyDictionary<string, FileSystemStoreOptions> ParsedStores { get; set; }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        public IReadOnlyDictionary<string, FileSystemScopedStoreOptions> ParsedScopedStores { get; set; }

        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindProviderInstanceOptions(FileSystemProviderInstanceOptions providerInstanceOptions)
        {
            if (string.IsNullOrEmpty(providerInstanceOptions.RootPath))
            {
                providerInstanceOptions.RootPath = this.RootPath;
            }
            else
            {
                if (!Path.IsPathRooted(providerInstanceOptions.RootPath))
                {
                    providerInstanceOptions.RootPath = Path.Combine(this.RootPath, providerInstanceOptions.RootPath);
                }
            }
        }

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        public void BindStoreOptions(FileSystemStoreOptions storeOptions, FileSystemProviderInstanceOptions providerInstanceOptions = null)
        {
            if (string.IsNullOrEmpty(storeOptions.RootPath))
            {
                if (providerInstanceOptions != null
                    && storeOptions.ProviderName == providerInstanceOptions.Name)
                {
                    storeOptions.RootPath = providerInstanceOptions.RootPath;
                }
                else
                {
                    storeOptions.RootPath = this.RootPath;
                }
            }
        }
    }
}
