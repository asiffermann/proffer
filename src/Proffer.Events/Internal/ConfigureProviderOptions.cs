namespace Proffer.Events.Internal
{
    using System.Linq;
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Events.Configuration;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// Configures a provider <typeparamref name="TParsedOptions"/> from generic <see cref="TQueueOptions"/>.
    /// </summary>
    /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
    /// <typeparam name="TProviderOptions">The type of the instance options.</typeparam>
    /// <typeparam name="TQueueOptions">The type of the queue options.</typeparam>
    /// <seealso cref="IConfigureOptions{TParsedOptions}" />
    public class ConfigureProviderOptions<TParsedOptions, TProviderOptions, TQueueOptions> : IConfigureOptions<TParsedOptions>
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TQueueOptions>
            where TProviderOptions : class, IProviderOptions, new()
            where TQueueOptions : class, IQueueOptions, new()

    {
        private readonly EventOptions eventOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureProviderOptions{TParsedOptions, TProviderOptions, TQueueOptions}"/> class.
        /// </summary>
        /// <param name="eventOptions">The event options.</param>
        public ConfigureProviderOptions(IOptions<EventOptions> eventOptions)
        {
            this.eventOptions = eventOptions.Value;
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TParsedOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(TParsedOptions options)
        {
            if (this.eventOptions == null)
            {
                return;
            }

            options.ConnectionStrings = this.eventOptions.ConnectionStrings;

            options.ProviderOptions = this.eventOptions.Providers.Parse<TProviderOptions>()
                .Where(kvp => kvp.Value.Type == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var providerOption in options.ProviderOptions)
            {
                providerOption.Value.Compute<TParsedOptions, TProviderOptions, TQueueOptions>(options);
            }

            var parsedQueues = this.eventOptions.Queues.Parse<TQueueOptions>();

            foreach (var queueOption in parsedQueues)
            {
                queueOption.Value.Compute<TParsedOptions, TProviderOptions, TQueueOptions>(options);
            }

            options.QueueOptions = parsedQueues
                .Where(kvp => kvp.Value.ProviderType == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
