namespace Proffer.Events.Internal
{
    using Microsoft.Extensions.Options;
    using Proffer.Events.Configuration;
    using Proffer.Events.Configuration.Queue;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Configuration;

    /// <summary>
    /// A abstract class to implement an EventsProvider 
    /// </summary>
    /// <typeparam name="TParsedOptions">The type of the parsed options. <see cref="IParsedOptions{TProviderOptions, TQueueOptions}"/> <seealso cref="EventOptions"/></typeparam>
    /// <typeparam name="TProviderOptions">The type of the provider options. <see cref="IEventProviderOptions"/></typeparam>
    /// <typeparam name="TQueueOptions">The type of the queue options. <see cref="IQueueOptions"/></typeparam>
    /// <seealso cref="IEventProvider" />
    public abstract class EventsProviderBase<TParsedOptions, TProviderOptions, TQueueOptions> : IEventProvider
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TQueueOptions>, new()
            where TProviderOptions : class, IProviderOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
    {
        /// <summary>
        /// The options
        /// </summary>
        protected readonly TParsedOptions Options;

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsProviderBase{TParsedOptions, TProviderOptions, TQueueOptions}"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EventsProviderBase(IOptions<TParsedOptions> options)
        {
            this.Options = options.Value;
        }

        /// <summary>
        /// Builds the queue provider.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <returns></returns>
        public IEventQueuer BuildQueueProvider(string queueName)
        {
            return this.BuildQueueInternal(queueName, this.Options.GetQueueConfiguration(queueName));
        }

        /// <summary>
        /// Builds the queue provider.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="queueOptions">The queue options.</param>
        /// <returns>
        /// An instance of <see cref="IEventQueuer"/>
        /// </returns>
        /// <exception cref="Exceptions.BadQueueProvider"></exception>
        public IEventQueuer BuildQueueProvider(string queueName, IQueueOptions queueOptions)
        {
            if (queueOptions.ProviderType != this.Name)
            {
                throw new Exceptions.BadQueueProvider(this.Name, queueName);
            }

            return this.BuildQueueInternal(queueName, queueOptions.ParseQueueOptions<TParsedOptions, TProviderOptions, TQueueOptions>(this.Options));
        }

        /// <summary>
        /// Builds the queue, a provider must overrid this abstract method.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="queueOptions">The queue options.</param>
        /// <returns>
        /// An instance of <see cref="IEventQueuer"/>
        /// </returns>
        protected abstract IEventQueuer BuildQueueInternal(string queueName, TQueueOptions queueOptions);
    }
}
