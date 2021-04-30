namespace Providers.Storage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// File common properties with metadata.
    /// </summary>
    public interface IFileProperties
    {
        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        DateTimeOffset? LastModified { get; }

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Gets the etag.
        /// </summary>
        string ETag { get; }

        /// <summary>
        /// Gets or sets the cache control.
        /// </summary>
        string CacheControl { get; set; }

        /// <summary>
        /// Gets the MD5 digest of the content.
        /// </summary>
        string ContentMD5 { get; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        IDictionary<string, string> Metadata { get; }
    }
}
