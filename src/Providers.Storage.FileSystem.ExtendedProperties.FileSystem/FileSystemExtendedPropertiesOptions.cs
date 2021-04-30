namespace Providers.Storage.FileSystem.ExtendedProperties.FileSystem
{
    /// <summary>
    /// Options for an <see cref="Internal.ExtendedPropertiesProvider"/>.
    /// </summary>
    public class FileSystemExtendedPropertiesOptions
    {
        /// <summary>
        /// Gets or sets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; } = ".{0}-extended-properties";
    }
}
