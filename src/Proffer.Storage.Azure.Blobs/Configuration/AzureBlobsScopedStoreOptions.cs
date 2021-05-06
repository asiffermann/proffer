namespace Proffer.Storage.Azure.Blobs.Configuration
{
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Options for a scoped <see cref="AzureBlobsStore"/>.
    /// </summary>
    /// <seealso cref="AzureBlobsStoreOptions" />
    /// <seealso cref="IScopedStoreOptions" />
    public class AzureBlobsScopedStoreOptions : AzureBlobsStoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// Gets the folder name format.
        /// </summary>
        public string FolderNameFormat { get; set; }
    }
}
