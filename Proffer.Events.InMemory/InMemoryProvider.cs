namespace Proffer.Events.InMemory
{
    using Microsoft.Extensions.Options;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Events.Configuration.Queue;
    using Proffer.Events.InMemory.Configuration;
    using Proffer.Events.InMemory.Internal;
    using Proffer.Events.Internal;

    /// <summary>
    /// An in memory queue provider
    /// </summary>
    /// <seealso cref="EventsProviderBase{InMemoryParsedOptions, InMemoryProviderInstanceOptions, InMemoryQueueOptions}" />
    public class InMemoryProvider : EventsProviderBase<InMemoryOptions, EventProviderOptions, QueueOptions>
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

        protected override IEventQueuer BuildQueueInternal(string queueName, QueueOptions queueOptions)
        {
            return new InMemoryEventQueuer(this.storedQueue, queueOptions);
        }
    }
}
