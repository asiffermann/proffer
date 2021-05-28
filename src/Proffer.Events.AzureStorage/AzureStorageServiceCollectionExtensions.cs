namespace Proffer.Events.AzureStorage
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Events.AzureStorage.Configuration;
    using Proffer.Events.Internal;

    /// <summary>
    /// Some azure storage extentions methods 
    /// </summary>
    public static class AzureStorageServiceCollectionExtensions
    {
        /// <summary>
        /// Inject the AzureStorageOptions parsed options in DI.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>
        ///  The IServiceCollection
        /// </returns>
        public static IServiceCollection AddAzureStorageQueue(this IServiceCollection services)
        {
            return services
                .AddScoped<IConfigureOptions<AzureStorageOptions>, ConfigureProviderOptions<AzureStorageOptions, AzureStorageProviderOptions, AzureStorageQueueOptions>>()
                .AddAzureStorageServices();
        }


        /// <summary>
        /// Inject the azure storage provider in DI.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>
        ///  The IServiceCollection
        /// </returns>
        public static IServiceCollection AddAzureStorageServices(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEventProvider, AzureStorageProvider>());
            return services;
        }


    }
}
