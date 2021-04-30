namespace Proffer.Storage
{
    using System;
    using FileSystem;
    using FileSystem.ExtendedProperties.FileSystem;
    using FileSystem.ExtendedProperties.FileSystem.Internal;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class FileSystemExtendedPropertiesExtensions
    {
        /// <summary>
        /// Registers a File System extended properties provider that stores it in JSON files.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The action to configure options.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddFileSystemExtendedProperties(this IServiceCollection services, Action<FileSystemExtendedPropertiesOptions> configure = null)
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
