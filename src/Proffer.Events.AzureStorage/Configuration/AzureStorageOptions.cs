namespace Proffer.Events.AzureStorage.Configuration
{
    using System.Collections.Generic;
    using Proffer.Events.Configuration;

    /// <summary>
    /// Azure storage event parsed options 
    /// </summary>
    /// <seealso cref="IParsedOptions{AzureStorageProviderInstanceOptions, AzureStorageQueueOptions}" />
    public class AzureStorageOptions : IParsedOptions<AzureStorageProviderOptions, AzureStorageQueueOptions>
    {
        /// <summary>
        /// Gets the name.
        /// </summary> 
        public string Name => AzureStorageProvider.ProviderName;

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider instances.
        /// </summary>
        public IReadOnlyDictionary<string, AzureStorageProviderOptions> ProviderOptions { get; set; }

        /// <summary>
        /// Gets or sets the parsed queue configuration.
        /// </summary>
        public IReadOnlyDictionary<string, AzureStorageQueueOptions> QueueOptions { get; set; }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        /// <exception cref="Exceptions.BadProviderConfiguration">The ConnectionString named '{providerOptions.Name}' cannot be found. Did you call AddEvent with the configuration root ?</exception>
        public void BindProviderOptions(AzureStorageProviderOptions providerOptions)
        {
            if (!string.IsNullOrEmpty(providerOptions.ConnectionStringName)
                && string.IsNullOrEmpty(providerOptions.ConnectionString))
            {
                if (!this.ConnectionStrings.ContainsKey(providerOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadProviderConfiguration(
                        providerOptions.Name,
                        $"The ConnectionString named '{providerOptions.Name}' cannot be found. Did you call AddEvent with the configuration root ?");
                }

                providerOptions.ConnectionString = this.ConnectionStrings[providerOptions.ConnectionStringName];
            }
        }

        /// <summary>
        /// Binds the queue options.
        /// </summary>
        /// <param name="queueOptions">The queue options.</param>
        /// <param name="providerOptions">The provider options.</param>
        /// <exception cref="Exceptions.BadQueueConfiguration">The ConnectionString named '{queueOptions.ConnectionStringName}' cannot be found. Did you call AddEvent with the configuration root ?</exception>
        public void BindQueueOptions(AzureStorageQueueOptions queueOptions, AzureStorageProviderOptions providerOptions = null)
        {
            if (!string.IsNullOrEmpty(queueOptions.ConnectionStringName)
                && string.IsNullOrEmpty(queueOptions.ConnectionStringName))
            {
                if (!this.ConnectionStrings.ContainsKey(queueOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadQueueConfiguration(
                        queueOptions.Name,
                        $"The ConnectionString named '{queueOptions.ConnectionStringName}' cannot be found. Did you call AddEvent with the configuration root ?");
                }

                queueOptions.ConnectionString = this.ConnectionStrings[queueOptions.ConnectionStringName];
            }

            if (providerOptions == null || queueOptions.ProviderName != providerOptions.Name)
            {
                return;
            }

            if (string.IsNullOrEmpty(queueOptions.ConnectionString))
            {
                queueOptions.ConnectionString = providerOptions.ConnectionString;
            }
        }
    }
}
