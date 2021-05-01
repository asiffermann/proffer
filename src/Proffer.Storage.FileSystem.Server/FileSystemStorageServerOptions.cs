namespace Proffer.Storage.FileSystem.Server
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Options for a <see cref="FileSystemStorageServerMiddleware"/>.
    /// </summary>
    public class FileSystemStorageServerOptions
    {
        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the endpoint path.
        /// </summary>
        public PathString EndpointPath { get; set; } = "/.well-known/storage";

        /// <summary>
        /// Gets or sets the signing key.
        /// </summary>
        public byte[] SigningKey { get; set; }
    }
}
