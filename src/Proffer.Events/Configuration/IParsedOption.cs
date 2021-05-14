namespace Proffer.Events.Configuration
{
    using System.Collections.Generic;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// Typed options parsed from the dynamic configuration.
    /// </summary>
    /// <typeparam name="TProviderOptions">The type of the provider instance options.</typeparam>
    /// <typeparam name="TQueueOptions">The type of the store options.</typeparam>
    public interface IParsedOptions<TProviderOptions, TQueueOptions>
        where TProviderOptions : class, IEventProviderOptions
        where TQueueOptions : class, IQueueOptions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider configuration.
        /// </summary>
        IReadOnlyDictionary<string, TProviderOptions> ProviderOptions { get; set; }

        /// <summary>
        /// Gets or sets the parsed queue configuration.
        /// </summary>
        IReadOnlyDictionary<string, TQueueOptions> QueueOptions { get; set; }

        /// <summary>
        /// Binds the provider options.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        void BindProviderOptions(TProviderOptions providerOptions);

        /// <summary>
        /// Binds the queue options.
        /// </summary>
        /// <param name="queueOptions">The queue options.</param>
        /// <param name="providerOptions">The provider options.</param>
        void BindQueueOptions(TQueueOptions queueOptions, TProviderOptions providerOptions = null);
    }
}
