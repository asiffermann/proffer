namespace Providers.Storage.Configuration
{
    public class ScopedStoreOptions : StoreOptions, IScopedStoreOptions
    {
        public string FolderNameFormat { get; set; }
    }
}
