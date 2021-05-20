namespace Proffer.Events.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Proffer.Configuration;
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// Extensions Methodes 
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static IReadOnlyDictionary<string, TOptions> Parse<TOptions>(this IReadOnlyDictionary<string, IConfigurationSection> unparsedConfiguration)
            where TOptions : class, INamedElementOptions, new()
        {
            if (unparsedConfiguration == null)
            {
                return new Dictionary<string, TOptions>();
            }

            return unparsedConfiguration
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => BindOptions<TOptions>(kvp));
        }

        public static void Compute<TParsedOptions, TProviderOptions, TQueueOptions>(this TProviderOptions parsedProviderInstance, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TQueueOptions>
            where TProviderOptions : class, IProviderOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            options.BindProviderOptions(parsedProviderInstance);
        }

        public static void Compute<TParsedOptions, TProviderOptions, TQueueOptions>(this TQueueOptions parsedQueue, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TQueueOptions>
            where TProviderOptions : class, IProviderOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            TProviderOptions providerOptions = null;
            if (!string.IsNullOrEmpty(parsedQueue.ProviderName))
            {
                options.ProviderOptions.TryGetValue(parsedQueue.ProviderName, out providerOptions);
                if (providerOptions == null)
                {
                    return;
                }

                parsedQueue.ProviderType = providerOptions.Type;
            }

            options.BindQueueOptions(parsedQueue, providerOptions);
        }

        private static TOptions BindOptions<TOptions>(KeyValuePair<string, IConfigurationSection> kvp)
            where TOptions : class, INamedElementOptions, new()
        {
            var options = new TOptions
            {
                Name = kvp.Key,
            };

            ConfigurationBinder.Bind(kvp.Value, options);
            return options;
        }

        public static TQueueOptions GetQueueConfiguration<TProviderOptions, TQueueOptions>(this IParsedOptions<TProviderOptions, TQueueOptions> parsedOptions, string queueName, bool throwIfNotFound = true)
            where TProviderOptions : class, IProviderOptions
            where TQueueOptions : class, IQueueOptions
        {
            parsedOptions.QueueOptions.TryGetValue(queueName, out var queueOptions);
            if (queueOptions != null)
            {
                return queueOptions;
            }

            if (throwIfNotFound)
            {
                throw new Exceptions.QueueNotFound(queueName);
            }

            return null;
        }

        public static TQueueOptions ParseQueueOptions<TParsedOptions, TProviderOptions, TQueueOptions>(this IQueueOptions queueOptions, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TQueueOptions>, new()
            where TProviderOptions : class, IProviderOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            if (!( queueOptions is TQueueOptions parsedQueueOptions ))
            {
                parsedQueueOptions = new TQueueOptions
                {
                    Name = queueOptions.Name,
                    ProviderName = queueOptions.ProviderName,
                    ProviderType = queueOptions.ProviderType
                };
            }

            parsedQueueOptions.Compute<TParsedOptions, TProviderOptions, TQueueOptions>(options);
            return parsedQueueOptions;
        }
    }
}
