namespace Proffer.Storage
{
    using Azure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Configuration;
    using Proffer.Storage.Internal;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Storage services to Azure Storage.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAzureStorage(this IServiceCollection services)
        {
            services
                .AddSingleton<IConfigureOptions<AzureParsedOptions>, ConfigureProviderOptions<AzureParsedOptions, AzureProviderInstanceOptions, AzureStoreOptions, AzureScopedStoreOptions>>()
                .TryAddEnumerable(ServiceDescriptor.Transient<IStorageProvider, AzureStorageProvider>());

            return services;
        }
    }
}
