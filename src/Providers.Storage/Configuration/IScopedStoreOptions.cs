namespace Providers.Storage.Configuration
{
    public interface IScopedStoreOptions : IStoreOptions
    {
        string FolderNameFormat { get; }
    }
}
