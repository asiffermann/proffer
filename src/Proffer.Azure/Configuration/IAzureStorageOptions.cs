namespace Proffer.Azure.Configuration
{
    /// <summary>
    /// Proffer options pointing to an Azure Storage account.
    /// </summary>
    public interface IAzureStorageOptions
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
