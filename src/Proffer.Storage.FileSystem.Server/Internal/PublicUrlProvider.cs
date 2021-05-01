namespace Proffer.Storage.FileSystem.Server.Internal
{
    using System;
    using FileSystem.Internal;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Provides a way to serve files from an HTTP URL on a File System using an ASP.NET middleware.
    /// </summary>
    /// <seealso cref="IPublicUrlProvider" />
    public class PublicUrlProvider : IPublicUrlProvider
    {
        private readonly FileSystemStorageServerOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicUrlProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PublicUrlProvider(IOptions<FileSystemStorageServerOptions> options)
        {
            this.options = options.Value;
        }

        /// <summary>
        /// Gets the public URL of a file reference.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// The public URL.
        /// </returns>
        public string GetPublicUrl(string storeName, FileSystemFileReference file)
        {
            var uriBuilder = new UriBuilder(this.options.BaseUri)
            {
                Path = this.options.EndpointPath.Add("/" + storeName).Add("/" + file.Path)
            };

            return uriBuilder.ToString();
        }
    }
}
