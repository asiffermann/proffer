namespace Providers.Storage.Azure.Configuration
{
    using Providers.Storage.Configuration;

    public class AzureScopedStoreOptions : AzureStoreOptions, IScopedStoreOptions
    {
        public string FolderNameFormat { get; set; }
    }
}
