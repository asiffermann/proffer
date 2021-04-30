namespace Providers.Storage.FileSystem.Configuration
{
    using Providers.Storage.Configuration;

    /// <summary>
    /// Options for a scoped <see cref="FileSystemStore"/>.
    /// </summary>
    /// <seealso cref="FileSystemStoreOptions" />
    /// <seealso cref="IScopedStoreOptions" />
    public class FileSystemScopedStoreOptions : FileSystemStoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// Gets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; }
    }
}
