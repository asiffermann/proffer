namespace Providers.Storage.Azure.Configuration
{
    using Providers.Storage.Configuration;

    /// <summary>
    /// Options for an instance of <see cref="AzureStorageProvider"/>.
    /// </summary>
    /// <seealso cref="ProviderInstanceOptions" />
    public class AzureProviderInstanceOptions : ProviderInstanceOptions
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
