namespace Providers.Storage.FileSystem.Configuration
{
    using Providers.Storage.Configuration;

    public class FileSystemScopedStoreOptions : FileSystemStoreOptions, IScopedStoreOptions
    {
        public string FolderNameFormat { get; set; }
    }
}
