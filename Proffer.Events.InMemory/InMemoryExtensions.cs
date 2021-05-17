namespace Proffer.Events.InMemory
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Events.Configuration.Provider;
    using Proffer.Events.Configuration.Queue;
    using Proffer.Events.InMemory.Configuration;
    using Proffer.Events.InMemory.Internal;
    using Proffer.Events.Internal;

    /// <summary>
    /// Some service collection extensions methods, to inject the in memory provider in DI.
    /// </summary>
    public static class InMemoryExtensions
    {
        /// <summary>
        /// Adds the in memory queue.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection AddInMemoryQueue(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConfigureOptions<InMemoryOptions>, ConfigureProviderOptions<InMemoryOptions, EventProviderOptions, QueueOptions>>()
                .AddInMemoryQueueServices()
                .AddInMemoryQueueStorage();
        }

        /// <summary>
        /// Adds the in memory queue services.
        /// </summary>
        /// <param name="services">The services.</param>
        internal static IServiceCollection AddInMemoryQueueServices(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Scoped<IEventProvider, InMemoryProvider>());
            return services;
        }

        /// <summary>
        /// Adds the in memory queue storage.
        /// </summary>
        /// <param name="services">The services.</param>
        internal static IServiceCollection AddInMemoryQueueStorage(this IServiceCollection services)
        {
            return services
                .AddSingleton<IQueueStorageInMemory, QueueStorageInMemory>();
        }

    }
}
