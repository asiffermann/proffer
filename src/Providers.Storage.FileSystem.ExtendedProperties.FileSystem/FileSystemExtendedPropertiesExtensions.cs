﻿namespace Providers.Storage
{
    using FileSystem;
    using FileSystem.ExtendedProperties.FileSystem;
    using FileSystem.ExtendedProperties.FileSystem.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class FileSystemExtendedPropertiesExtensions
    {
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
