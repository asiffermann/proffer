namespace Providers.Storage
{
    using Azure;
    using Providers.Storage.Azure.Configuration;
    using Providers.Storage.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class AzureStorageExtensions
    {
        public static IServiceCollection AddAzureStorage(this IServiceCollection services)
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
