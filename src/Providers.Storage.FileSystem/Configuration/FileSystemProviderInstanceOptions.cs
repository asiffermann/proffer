namespace Providers.Storage.FileSystem.Configuration
{
    using Providers.Storage.Configuration;

    /// <summary>
    /// Options for an instance of <see cref="FileSystemStorageProvider"/>.
    /// </summary>
    /// <seealso cref="ProviderInstanceOptions" />
    public class FileSystemProviderInstanceOptions : ProviderInstanceOptions
    {
        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        public string RootPath { get; set; }
    }
}
