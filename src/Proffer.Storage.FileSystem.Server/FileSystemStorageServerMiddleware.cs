namespace Proffer.Storage.FileSystem.Server
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.FileSystem.Configuration;

    /// <summary>
    /// ASP.NET Core middleware to serve over HTTP files stored in a Storage store.
    /// </summary>
    public class FileSystemStorageServerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<FileSystemStorageServerMiddleware> logger;
        private readonly IOptions<FileSystemStorageServerOptions> serverOptions;
        private readonly FileSystemParsedOptions fileSystemParsedOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorageServerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next function.</param>
        /// <param name="serverOptions">The server options.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="fileSystemParsedOptions">The file system parsed options.</param>
        public FileSystemStorageServerMiddleware(
            RequestDelegate next,
            IOptions<FileSystemStorageServerOptions> serverOptions,
            ILogger<FileSystemStorageServerMiddleware> logger,
            IOptions<FileSystemParsedOptions> fileSystemParsedOptions)
        {
            this.fileSystemParsedOptions = fileSystemParsedOptions.Value;
            this.next = next;
            this.serverOptions = serverOptions;
            this.logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            int subPathStart = context.Request.Path.Value.IndexOf('/', 1);
            if (subPathStart > 0)
            {
                string storeName = context.Request.Path.Value.Substring(1, subPathStart - 1);
                IStorageFactory storageFactory = context.RequestServices.GetRequiredService<IStorageFactory>();

                if (this.fileSystemParsedOptions.ParsedStores.TryGetValue(storeName, out FileSystemStoreOptions storeOptions)
                    && storeOptions.ProviderType == FileSystemStorageProvider.ProviderName)
                {
                    if (storeOptions.AccessLevel != Storage.Configuration.AccessLevel.Public)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }

                    IStore store = storageFactory.GetStore(storeName, storeOptions);

                    IFileReference file = await store.GetAsync(context.Request.Path.Value.Substring(subPathStart + 1), withMetadata: true);
                    if (file != null)
                    {
                        context.Response.ContentType = file.Properties.ContentType;
                        context.Response.StatusCode = StatusCodes.Status200OK;

                        if (!string.IsNullOrEmpty(file.Properties.ETag))
                        {
                            context.Response.Headers.Add("ETag", new[] { file.Properties.ETag });
                        }

                        await file.ReadToStreamAsync(context.Response.Body);
                        return;
                    }
                }
            }

            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
    }
}
