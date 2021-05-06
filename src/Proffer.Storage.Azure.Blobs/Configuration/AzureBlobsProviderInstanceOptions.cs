namespace Proffer.Storage.Azure.Blobs.Configuration
{
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Options for an instance of <see cref="AzureBlobsStorageProvider"/>.
    /// </summary>
    /// <seealso cref="ProviderInstanceOptions" />
    public class AzureBlobsProviderInstanceOptions : ProviderInstanceOptions
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
