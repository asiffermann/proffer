namespace Proffer.Storage
{
    using System;
    using FileSystem;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.FileSystem.Properties.Json;
    using Proffer.Storage.FileSystem.Properties.Json.Internal;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class FileSystemPropertiesJsonServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a File System extended properties provider that stores it in JSON files.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The action to configure options.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddFileSystemExtendedProperties(this IServiceCollection services, Action<FileSystemPropertiesJsonOptions> configure = null)
        {
            if (configure == null)
            {
                configure = o => { };
            }

            services.Configure(configure);
            services.AddTransient<IExtendedPropertiesProvider, ExtendedPropertiesProvider>();
            return services;
        }
    }
}
