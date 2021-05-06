namespace Proffer.Storage.Azure.Blobs.Configuration
{
    using Proffer.Azure.Configuration;
    using Proffer.Configuration;

    /// <summary>
    /// Options for an <see cref="AzureBlobsStorageProvider"/>.
    /// </summary>
    /// <seealso cref="ProviderOptions" />
    public class AzureBlobsProviderOptions : ProviderOptions, IAzureStorageOptions
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the connection string to reference.
        /// </summary>
        public string ConnectionStringName { get; set; }
    }
}
