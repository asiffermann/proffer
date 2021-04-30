namespace Providers.Storage.FileSystem.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Extends standard file properties to match the requirements of <see cref="IFileProperties"/>.
    /// </summary>
    public class FileExtendedProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileExtendedProperties"/> class.
        /// </summary>
        public FileExtendedProperties()
        {
            this.Metadata = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the etag.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the cache control.
        /// </summary>
        public string CacheControl { get; set; }

        /// <summary>
        /// Gets or sets the MD5 digest of the content.
        /// </summary>
        public string ContentMD5 { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
