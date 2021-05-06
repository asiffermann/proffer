namespace Proffer.Storage.FileSystem.Configuration
{
    using Proffer.Configuration;

    /// <summary>
    /// Options for a <see cref="FileSystemStorageProvider"/>.
    /// </summary>
    /// <seealso cref="ProviderOptions" />
    public class FileSystemProviderOptions : ProviderOptions
    {
        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        public string RootPath { get; set; }
    }
}
