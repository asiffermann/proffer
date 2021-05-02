namespace Proffer.Storage.FileSystem.Properties.Json
{
    /// <summary>
    /// Options for an <see cref="Internal.ExtendedPropertiesProvider"/>.
    /// </summary>
    public class FileSystemPropertiesJsonOptions
    {
        /// <summary>
        /// Gets or sets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; } = ".{0}-extended-properties";
    }
}
