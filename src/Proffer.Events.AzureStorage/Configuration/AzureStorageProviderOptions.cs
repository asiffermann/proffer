namespace Proffer.Events.AzureStorage.Configuration
{
    using Proffer.Configuration;

    /// <summary>
    /// An azure storage provider options
    /// </summary>
    /// <seealso cref="ProviderOptions" />
    public class AzureStorageProviderOptions : ProviderOptions
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        public string ConnectionStringName { get; set; }
    }
}
