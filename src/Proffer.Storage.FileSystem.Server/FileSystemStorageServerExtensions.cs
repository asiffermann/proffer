namespace Proffer.Storage
{
    using System;
    using FileSystem;
    using FileSystem.Server;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class FileSystemStorageServerExtensions
    {
        /// <summary>
        /// Adds a File System provider Storage Server, serving files over HTTP.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The action to configure options.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileSystemStorageServer(this IServiceCollection services, Action<FileSystemStorageServerOptions> configure)
        {
            services.Configure(configure);
            services.AddTransient<IPublicUrlProvider, FileSystem.Server.Internal.PublicUrlProvider>();
            return services;
        }

        /// <summary>
        /// Adds a <see cref="FileSystemStorageServerMiddleware"/> to the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseFileSystemStorageServer(this IApplicationBuilder app)
        {
            IOptions<FileSystemStorageServerOptions> options = app.ApplicationServices.GetRequiredService<IOptions<FileSystemStorageServerOptions>>();
            app.Map(options.Value.EndpointPath, storePipeline =>
            {
                storePipeline.UseMiddleware<FileSystemStorageServerMiddleware>();
            });

            return app;
        }
    }
}
