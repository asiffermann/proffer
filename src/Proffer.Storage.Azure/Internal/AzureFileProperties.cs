namespace Proffer.Storage.Azure.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// File common properties with metadata stored on Azure Storage.
    /// </summary>
    /// <seealso cref="IFileProperties" />
    public class AzureFileProperties : IFileProperties
    {
        private const string DefaultCacheControl = "max-age=300, must-revalidate";
        private readonly ICloudBlob cloudBlob;
        private readonly Dictionary<string, string> decodedMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileProperties"/> class.
        /// </summary>
        /// <param name="cloudBlob">The Azure Storage blob.</param>
        public AzureFileProperties(ICloudBlob cloudBlob)
        {
            this.cloudBlob = cloudBlob;
            if (string.IsNullOrEmpty(this.cloudBlob.Properties.CacheControl))
            {
                this.cloudBlob.Properties.CacheControl = DefaultCacheControl;
            }

            if (this.cloudBlob.Metadata != null)
            {
                this.decodedMetadata = this.cloudBlob.Metadata.ToDictionary(m => m.Key, m => WebUtility.HtmlDecode(m.Value));
            }
            else
            {
                this.decodedMetadata = new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        public DateTimeOffset? LastModified => this.cloudBlob.Properties.LastModified;

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        public long Length => this.cloudBlob.Properties.Length;

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        public string ContentType
        {
            get => this.cloudBlob.Properties.ContentType;
            set => this.cloudBlob.Properties.ContentType = value;
        }

        /// <summary>
        /// Gets the etag.
        /// </summary>
        public string ETag => this.cloudBlob.Properties.ETag;

        /// <summary>
        /// Gets or sets the cache control.
        /// </summary>
        public string CacheControl
        {
            get => this.cloudBlob.Properties.CacheControl;
            set => this.cloudBlob.Properties.CacheControl = value;
        }

        /// <summary>
        /// Gets the MD5 digest of the content.
        /// </summary>
        public string ContentMD5 => this.cloudBlob.Properties.ContentMD5;

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public IDictionary<string, string> Metadata => this.decodedMetadata;

        internal async Task SaveAsync()
        {
            await this.cloudBlob.SetPropertiesAsync();

            foreach (KeyValuePair<string, string> meta in this.decodedMetadata)
            {
                this.cloudBlob.Metadata[meta.Key] = WebUtility.HtmlEncode(meta.Value);
            }

            await this.cloudBlob.SetMetadataAsync();
        }
    }
}
