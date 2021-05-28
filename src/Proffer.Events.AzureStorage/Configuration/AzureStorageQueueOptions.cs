namespace Proffer.Events.AzureStorage.Configuration
{
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// Azure storage queue options
    /// </summary>
    /// <seealso cref="QueueOptions" />
    public class AzureStorageQueueOptions : QueueOptions
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
