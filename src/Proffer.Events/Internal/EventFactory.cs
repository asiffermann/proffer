namespace Proffer.Events.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Options;
    using Proffer.Events.Configuration;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// Default event factory to build <see cref="IStore"/> from configured <see cref="IStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IEventFactory" />
    public class EventFactory : IEventFactory
    {
        private readonly EventOptions options;
        private readonly IReadOnlyDictionary<string, IEventProvider> queueProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFactory"/> class.
        /// </summary>
        /// <param name="queueProviders">The queue providers.</param>
        /// <param name="options">The options.</param>
        public EventFactory(IEnumerable<IEventProvider> queueProviders, IOptions<EventOptions> options)
        {
            this.queueProviders = queueProviders.ToDictionary(qp => qp.Name, qp => qp);
            this.options = options.Value;
        }

        /// <summary>
        /// Gets a queuer with specific option.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// A configured <see cref=IEventQueuer"/>
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFound"></exception>
        public IEventQueuer GetQueuer(string queueName, IQueueOptions configuration)
        {
            return this.GetProvider(configuration).BuildQueueProvider(queueName, configuration);
        }

        /// <summary>
        /// Gets a queuer from configured options.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <returns>
        /// A configured <see cref="IEventQueuer"/>
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFound"></exception>
        /// <exception cref="Exceptions.QueueNotFound"></exception>
        public IEventQueuer GetQueuer(string queueName)
        {
            return this.GetProvider(this.options.GetQueueConfiguration(queueName)).BuildQueueProvider(queueName);
        }

        /// <summary>
        /// Gets a queuer from configured options.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="queuer">When this method returns <c>true</c>, contains the store associated with the specified name, if it is found in the <see cref="EventOptions" />; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>
        ///     <c>true</c> if the queuer was configured and built from its provider; otherwise, false.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFound"></exception>
        /// <exception cref="Exceptions.QueueNotFound"></exception>
        public bool TryGetQueuer(string queueName, out IEventQueuer queuer)
        {
            var configuration = this.options.GetQueueConfiguration(queueName, throwIfNotFound: false);
            if (configuration != null)
            {
                var provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null)
                {
                    queuer = provider.BuildQueueProvider(queueName);
                    return true;
                }
            }

            queuer = null;
            return false;
        }

        /// <summary>
        /// Gets a queuer from configured options.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="queuer">When this method returns <c>true</c>, contains the store associated with the specified name, if it is found in the <see cref="EventOptions" />; otherwise, null. This parameter is passed uninitialized.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>
        ///     <c>true</c> if the queuer was configured and built from its provider; otherwise, false.
        /// </returns>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        /// <exception cref="Exceptions.ProviderNotFound"></exception>
        /// <exception cref="Exceptions.QueueNotFound"></exception>
        public bool TryGetQueuer(string queueName, out IEventQueuer queuer, string providerName)
        {
            var configuration = this.options.GetQueueConfiguration(queueName, throwIfNotFound: false);
            if (configuration != null)
            {
                var provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null && provider.Name == providerName)
                {
                    queuer = provider.BuildQueueProvider(queueName);
                    return true;
                }
            }

            queuer = null;
            return false;
        }

        private IEventProvider GetProvider(IQueueOptions configuration, bool throwIfNotFound = true)
        {
            string providerTypeName = null;
            if (!string.IsNullOrEmpty(configuration.ProviderType))
            {
                providerTypeName = configuration.ProviderType;
            }
            else if (!string.IsNullOrEmpty(configuration.ProviderName))
            {
                this.options.ProviderOptions.TryGetValue(configuration.ProviderName, out Configuration.Provider.EventProviderOptions providersInstanceOptions);
                if (providersInstanceOptions != null)
                {
                    providerTypeName = providersInstanceOptions.Type;
                }
                else if (throwIfNotFound)
                {
                    throw new Exceptions.BadProviderConfiguration(configuration.ProviderName, "Unable to find it in the configuration ");
                }
            }
            else if (throwIfNotFound)
            {
                throw new Exceptions.BadQueueConfiguration(configuration.Name, "You must set either 'ProviderType or 'ProviderName' in event configuration.");
            }

            if (string.IsNullOrEmpty(providerTypeName))
            {
                return null;
            }

            this.queueProviders.TryGetValue(providerTypeName, out IEventProvider provider);
            if (provider == null && throwIfNotFound)
            {
                throw new Exceptions.ProviderNotFound(providerTypeName);
            }

            return provider;
        }
    }
}
