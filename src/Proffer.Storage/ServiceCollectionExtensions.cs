namespace Proffer.Storage
{
    using System.Collections.Generic;
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Proffer.Storage services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddStorage(this IServiceCollection services)
        {
            services.TryAddTransient<IStorageFactory, Internal.StorageFactory>();
            services.TryAdd(ServiceDescriptor.Transient(typeof(IStore<>), typeof(Internal.GenericStoreProxy<>)));
            return services;
        }

        /// <summary>
        /// Registers Proffer.Storage services and configures it with the given section.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configurationSection">The configuration section.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            return services
                .Configure<StorageOptions>(configurationSection)
                .AddStorage();
        }

        /// <summary>
        /// Registers Proffer.Storage services and configures it from the given <paramref name="configurationRoot" /> at section <paramref name="sectionName" />.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <param name="sectionName">The name of the section.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfigurationRoot configurationRoot, string sectionName = StorageOptions.DefaultConfigurationSectionName)
        {
            return services
                .Configure<StorageOptions>(configurationRoot.GetSection(sectionName))
                .Configure<StorageOptions>(storageOptions =>
                {
                    var connectionStrings = new Dictionary<string, string>();
                    ConfigurationBinder.Bind(configurationRoot.GetSection("ConnectionStrings"), connectionStrings);

                    if (storageOptions.ConnectionStrings != null)
                    {
                        foreach (KeyValuePair<string, string> existingConnectionString in storageOptions.ConnectionStrings)
                        {
                            connectionStrings[existingConnectionString.Key] = existingConnectionString.Value;
                        }
                    }

                    storageOptions.ConnectionStrings = connectionStrings;
                })
                .AddStorage();
        }
    }
}
