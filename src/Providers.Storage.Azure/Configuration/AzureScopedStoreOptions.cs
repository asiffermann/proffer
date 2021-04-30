namespace Providers.Storage.Azure.Configuration
{
    using Providers.Storage.Configuration;

    /// <summary>
    /// Options for a scoped <see cref="AzureStore"/>.
    /// </summary>
    /// <seealso cref="AzureStoreOptions" />
    /// <seealso cref="IScopedStoreOptions" />
    public class AzureScopedStoreOptions : AzureStoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// Gets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; }
    }
}
