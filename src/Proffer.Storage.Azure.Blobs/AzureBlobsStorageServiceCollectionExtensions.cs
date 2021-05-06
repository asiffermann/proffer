namespace Proffer.Storage
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Blobs;
    using Proffer.Storage.Azure.Blobs.Configuration;
    using Proffer.Storage.Internal;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class AzureBlobsStorageServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Storage services to Azure Blobs.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAzureStorage(this IServiceCollection services)
        {
            services
                .AddSingleton<IConfigureOptions<AzureBlobsParsedOptions>, ConfigureProviderOptions<AzureBlobsParsedOptions, AzureBlobsProviderInstanceOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions>>()
                .TryAddEnumerable(ServiceDescriptor.Transient<IStorageProvider, AzureBlobsStorageProvider>());

            return services;
        }
    }
}
