namespace Providers.Storage.FileSystem.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// File common properties with metadata stored on a File System.
    /// </summary>
    /// <seealso cref="IFileProperties" />
    public class FileSystemFileProperties : IFileProperties
    {
        private readonly FileInfo fileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemFileProperties"/> class.
        /// </summary>
        /// <param name="fileSystemPath">The file system path.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        public FileSystemFileProperties(string fileSystemPath, FileExtendedProperties extendedProperties)
        {
            this.fileInfo = new FileInfo(fileSystemPath);
            this.ExtendedProperties = extendedProperties;
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        public DateTimeOffset? LastModified => new DateTimeOffset(this.fileInfo.LastWriteTimeUtc, TimeZoneInfo.Local.BaseUtcOffset);

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        public long Length => this.fileInfo.Length;

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        public string ContentType
        {
            get => this.ExtendedProperties.ContentType;
            set => this.ExtendedProperties.ContentType = value;
        }

        /// <summary>
        /// Gets the etag.
        /// </summary>
        public string ETag => this.ExtendedProperties.ETag;

        /// <summary>
        /// Gets or sets the cache control.
        /// </summary>
        public string CacheControl
        {
            get => this.ExtendedProperties.CacheControl;
            set => this.ExtendedProperties.CacheControl = value;
        }

        /// <summary>
        /// Gets the MD5 digest of the content.
        /// </summary>
        public string ContentMD5 => this.ExtendedProperties.ContentMD5;

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public IDictionary<string, string> Metadata => this.ExtendedProperties.Metadata;

        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        internal FileExtendedProperties ExtendedProperties { get; }
    }
}
