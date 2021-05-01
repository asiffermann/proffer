namespace Proffer.Storage
{
    using FileSystem;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.FileSystem.Configuration;
    using Proffer.Storage.Internal;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Storage services to the File System on the given root path.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="rootPath">The root path.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileSystemStorage(this IServiceCollection services, string rootPath)
        {
            return services
                .Configure<FileSystemParsedOptions>(options => options.RootPath = rootPath)
                .AddFileSystemStorageServices();
        }

        /// <summary>
        /// Registers the Proffer.Storage services to the File System on the root path <see cref="System.IO.Directory.GetCurrentDirectory"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileSystemStorage(this IServiceCollection services)
        {
            return services              
                .Configure<FileSystemParsedOptions>(options => options.RootPath = System.IO.Directory.GetCurrentDirectory())
                .AddFileSystemStorageServices();
        }

        private static IServiceCollection AddFileSystemStorageServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<FileSystemParsedOptions>, ConfigureProviderOptions<FileSystemParsedOptions, FileSystemProviderInstanceOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions>>();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IStorageProvider, FileSystemStorageProvider>());
            return services;
        }
    }
}
