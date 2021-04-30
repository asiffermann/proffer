namespace Providers.Storage.Configuration
{
    /// <summary>
    /// Options for a scoped <see cref="IStore"/>.
    /// </summary>
    /// <seealso cref="IStoreOptions" />
    public interface IScopedStoreOptions : IStoreOptions
    {
        /// <summary>
        /// Gets the folder name format.
        /// </summary>
        string FolderNameFormat { get; }
    }
}
