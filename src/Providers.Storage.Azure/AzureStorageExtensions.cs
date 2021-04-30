namespace Providers.Storage
{
    using Azure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Providers.Storage.Azure.Configuration;
    using Providers.Storage.Internal;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class AzureStorageExtensions
    {
        /// <summary>
        /// Registers the Azure Storage provider services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAzureStorageProvider(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConfigureOptions<AzureParsedOptions>, ConfigureProviderOptions<AzureParsedOptions, AzureProviderInstanceOptions, AzureStoreOptions, AzureScopedStoreOptions>>()
                .AddAzureStorageServices();
        }

        private static IServiceCollection AddAzureStorageServices(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IStorageProvider, AzureStorageProvider>());
            return services;
        }
    }
}
