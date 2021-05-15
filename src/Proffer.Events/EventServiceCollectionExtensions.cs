namespace Proffer.Events
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Proffer.Events.Configuration;

    /// <summary>
    /// Some service collection extensions methods.
    /// </summary>
    public static class EventServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddEvent(this IServiceCollection services)
        {
            services.TryAddTransient<IEventFactory, Internal.EventFactory>();
            return services;
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configurationSection">The configuration section.</param>
        /// <returns></returns>
        public static IServiceCollection AddEvent(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            return services
                .Configure<EventOptions>(configurationSection)
                .AddEvent();
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <returns></returns>
        public static IServiceCollection AddEvent(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            return services
                .Configure<EventOptions>(configurationRoot.GetSection(EventOptions.DefaultConfigurationSectionName))
                .Configure<EventOptions>(eventOptions =>
                {
                    var connectionStrings = new Dictionary<string, string>();
                    ConfigurationBinder.Bind(configurationRoot.GetSection("ConnectionStrings"), connectionStrings);

                    if (eventOptions.ConnectionStrings != null)
                    {
                        foreach (var existingConnectionString in eventOptions.ConnectionStrings)
                        {
                            connectionStrings[existingConnectionString.Key] = existingConnectionString.Value;
                        }
                    }

                    eventOptions.ConnectionStrings = connectionStrings;
                })
                .AddEvent();
        }

        /// <summary>
        /// Adds the event receiver.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddEventReceiver(this IServiceCollection services)
        {
            services.AddScoped<IEventReceiver, EventReceiver>();
        }

    }
}
