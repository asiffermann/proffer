namespace Proffer.Events.InMemory
{
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Events.Configuration.Queue;
    using Proffer.Events.InMemory.Configuration;
    using Proffer.Events.InMemory.Internal;
    using Proffer.Events.Internal;

    /// <summary>
    /// An in memory queue provider
    /// </summary>
    /// <seealso cref="EventsProviderBase{InMemoryParsedOptions, InMemoryProviderInstanceOptions, InMemoryQueueOptions}" />
    public class InMemoryProvider : EventsProviderBase<InMemoryOptions, ProviderOptions, QueueOptions>
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public const string ProviderName = "InMemory";

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public override string Name => ProviderName;

        /// <summary>
        /// The stored queue
        /// </summary>
        private readonly IQueueStorageInMemory storedQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryProvider"/> class.
        /// </summary>
        /// <param name="storedQueue">The stored queue.</param>
        /// <param name="options">The options.</param>
        public InMemoryProvider(IQueueStorageInMemory storedQueue, IOptions<InMemoryOptions> options)
            : base(options)
        {
            this.storedQueue = storedQueue;
        }

        /// <summary>
        /// Builds the queue, a provider must overrid this abstract method.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="queueOptions">The queue options.</param>
        /// <returns>
        /// An instance of <see cref="IEventQueuer" />
        /// </returns>
        protected override IEventQueuer BuildQueueInternal(string queueName, QueueOptions queueOptions)
        {
            return new InMemoryEventQueuer(this.storedQueue, queueOptions);
        }
    }
}
