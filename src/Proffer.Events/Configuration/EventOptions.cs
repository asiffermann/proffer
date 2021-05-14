namespace Proffer.Events.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// The Proffer.Events options with providers and stores 
    /// </summary>
    /// <seealso cref="IParsedOptions{EventProviderOptions, QueueOptions}" />
    public class EventOptions : IParsedOptions<EventProviderOptions, QueueOptions>
    {
        private const string DefaultConfigurationSectionName = "Event";

        private readonly Lazy<IReadOnlyDictionary<string, EventProviderOptions>> parsedProviderInstances;
        private readonly Lazy<IReadOnlyDictionary<string, QueueOptions>> parsedQueueOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventOptions"/> class.
        /// </summary>
        public EventOptions()
        {
            this.parsedProviderInstances = new Lazy<IReadOnlyDictionary<string, EventProviderOptions>>(
                () => this.ProviderConfigurations.Parse<EventProviderOptions>());
            this.parsedQueueOptions = new Lazy<IReadOnlyDictionary<string, QueueOptions>>(
                () => this.QueueConfigurations.Parse<QueueOptions>());
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => DefaultConfigurationSectionName;

        /// <summary>
        /// Gets or sets the providers unparsed options.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationSection> ProviderConfigurations { get; set; }

        /// <summary>
        /// Gets or sets the queues unparsed options.
        /// </summary>
        public IReadOnlyDictionary<string, IConfigurationSection> QueueConfigurations { get; set; }

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the queue options.
        /// </summary>
        public IReadOnlyDictionary<string, QueueOptions> QueueOptions { get => this.parsedQueueOptions.Value; set { } }

        /// <summary>
        /// Gets or sets the provider options.
        /// </summary>
        public IReadOnlyDictionary<string, EventProviderOptions> ProviderOptions { get => this.parsedProviderInstances.Value; set { } }

        /// <summary>
        /// Binds the queue options.
        /// </summary>
        /// <param name="queueOptions">The queue options.</param>
        /// <param name="providerOptions">The provider options.</param>
        public void BindQueueOptions(QueueOptions queueOptions, EventProviderOptions providerOptions) { }

        /// <summary>
        /// Binds the provider options.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        public void BindProviderOptions(EventProviderOptions providerOptions) { }
    }
}
