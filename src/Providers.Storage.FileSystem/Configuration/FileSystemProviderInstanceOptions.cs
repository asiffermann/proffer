namespace Providers.Storage.FileSystem.Configuration
{
    using Providers.Storage.Configuration;

    public class FileSystemProviderInstanceOptions : ProviderInstanceOptions
    {
        public string RootPath { get; set; }
    }
}
