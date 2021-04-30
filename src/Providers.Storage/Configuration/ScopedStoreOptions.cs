namespace Providers.Storage.Configuration
{
    /// <summary>
    /// Generic options for a scoped <see cref="IStore"/>.
    /// </summary>
    /// <seealso cref="StoreOptions" />
    /// <seealso cref="IScopedStoreOptions" />
    public class ScopedStoreOptions : StoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// Gets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; }
    }
}
