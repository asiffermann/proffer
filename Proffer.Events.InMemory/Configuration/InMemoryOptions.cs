namespace Proffer.Events.InMemory.Configuration
{
    using System.Collections.Generic;
    using Proffer.Events.Configuration;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// This class represent the InMemory provider parsed options.
    /// </summary>
    /// <seealso cref="IParsedOptions{EventProviderOptions, QueueOptions}" />
    public class InMemoryOptions : IParsedOptions<EventProviderOptions, QueueOptions>
    {
        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public string Name => InMemoryProvider.ProviderName;

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed queue configuration.
        /// </summary>
        public IReadOnlyDictionary<string, QueueOptions> QueueOptions { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider configuration.
        /// </summary>
        public IReadOnlyDictionary<string, EventProviderOptions> ProviderOptions { get; set; }

        /// <summary>
        /// Binds the provider options.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        public void BindProviderOptions(EventProviderOptions providerOptions) { }

        /// <summary>
        /// Binds the queue options.
        /// </summary>
        /// <param name="queueOptions">The queue options.</param>
        /// <param name="providerOptions">The provider options.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BindQueueOptions(QueueOptions queueOptions, EventProviderOptions providerOptions = null) { }
    }
}
