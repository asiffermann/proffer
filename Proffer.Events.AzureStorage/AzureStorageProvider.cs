namespace Proffer.Events.AzureStorage
{
    using Microsoft.Extensions.Options;
    using Proffer.Events.AzureStorage.Configuration;
    using Proffer.Events.Internal;

    /// <summary>
    /// An in azure storage queue provider
    /// </summary>
    /// <seealso cref="EventsProviderBase{AzureStorageOptions, AzureStorageProviderOptions, AzureStorageQueueOptions}" />
    public class AzureStorageProvider : EventsProviderBase<AzureStorageOptions, AzureStorageProviderOptions, AzureStorageQueueOptions>
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public const string ProviderName = "AzureStorage";

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public override string Name => ProviderName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureStorageProvider(IOptions<AzureStorageOptions> options)
            : base(options)
        {
        }

        /// <summary>
        /// Builds the queue, a provider must overrid this abstract method.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="queueOptions">The queue options.</param>
        /// <returns>
        /// An instance of <see cref="IEventQueuer" />
        /// </returns>
        protected override IEventQueuer BuildQueueInternal(string queueName, AzureStorageQueueOptions queueOptions)
        {
            return new AzureStorageEventQueuer(queueOptions);
        }
    }
}
