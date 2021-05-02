namespace Proffer.Storage.Azure.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;

    /// <summary>
    /// File common properties with metadata stored on Azure Storage.
    /// </summary>
    /// <seealso cref="IFileProperties" />
    public class AzureFileProperties : IFileProperties
    {
        private const string DefaultCacheControl = "max-age=300, must-revalidate";
        private readonly BlobClient blobClient;
        private BlobHttpHeaders blobHeaders;
        private Dictionary<string, string> decodedMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileProperties" /> class.
        /// </summary>
        /// <param name="blobClient">The Azure Storage blob client.</param>
        /// <param name="blobProperties">The blob properties.</param>
        public AzureFileProperties(BlobClient blobClient, BlobProperties blobProperties)
        {
            this.blobClient = blobClient;
            this.ExtractProperties(blobProperties);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileProperties"/> class.
        /// </summary>
        /// <param name="blobClient">The Azure Storage blob client.</param>
        /// <param name="blobItem">The blob item from listing.</param>
        public AzureFileProperties(BlobClient blobClient, BlobItem blobItem)
        {
            this.blobClient = blobClient;
            this.ExtractProperties(blobItem);
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        public DateTimeOffset? LastModified { get; private set; }

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        public string ContentType
        {
            get => this.blobHeaders.ContentType;
            set => this.blobHeaders.ContentType = value;
        }

        /// <summary>
        /// Gets the etag.
        /// </summary>
        public string ETag { get; private set; }

        /// <summary>
        /// Gets or sets the cache control.
        /// </summary>
        public string CacheControl
        {
            get => this.blobHeaders.CacheControl;
            set => this.blobHeaders.CacheControl = value;
        }

        /// <summary>
        /// Gets the MD5 digest of the content.
        /// </summary>
        public string ContentMD5 { get; private set; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public IDictionary<string, string> Metadata => this.decodedMetadata;

        internal async Task SaveAsync()
        {
            await this.blobClient.SetHttpHeadersAsync(this.blobHeaders);

            var metadata = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> meta in this.decodedMetadata)
            {
                metadata[meta.Key] = WebUtility.HtmlEncode(meta.Value);
            }

            await this.blobClient.SetMetadataAsync(metadata);

            Response<BlobProperties> refreshedProperties = await this.blobClient.GetPropertiesAsync();
            this.ExtractProperties(refreshedProperties);
        }

        private void ExtractProperties(BlobProperties blobProperties)
            => this.ComputeProperties(
                blobProperties.CacheControl,
                blobProperties.ContentType,
                blobProperties.ContentHash,
                blobProperties.ContentEncoding,
                blobProperties.ContentLanguage,
                blobProperties.ContentDisposition,
                blobProperties.LastModified,
                blobProperties.ContentLength,
                blobProperties.ETag,
                blobProperties.Metadata);

        private void ExtractProperties(BlobItem blobItem)
            => this.ComputeProperties(
                blobItem.Properties.CacheControl,
                blobItem.Properties.ContentType,
                blobItem.Properties.ContentHash,
                blobItem.Properties.ContentEncoding,
                blobItem.Properties.ContentLanguage,
                blobItem.Properties.ContentDisposition,
                blobItem.Properties.LastModified,
                blobItem.Properties.ContentLength,
                blobItem.Properties.ETag,
                blobItem.Metadata);

        private void ComputeProperties(
            string cacheControl,
            string contentType,
            byte[] contentHash,
            string contentEncoding,
            string contentLanguage,
            string contentDisposition,
            DateTimeOffset? lastModified,
            long? contentLength,
            ETag? etag,
            IDictionary<string, string> metadata)
        {
            this.blobHeaders = new BlobHttpHeaders
            {
                CacheControl = cacheControl ?? DefaultCacheControl,
                ContentType = contentType,
                ContentHash = contentHash,
                ContentEncoding = contentEncoding,
                ContentLanguage = contentLanguage,
                ContentDisposition = contentDisposition,
            };

            if (contentHash != null)
            {
                this.ContentMD5 = Convert.ToBase64String(contentHash);
            }

            this.LastModified = lastModified;
            this.Length = contentLength.GetValueOrDefault();

            if (etag.HasValue)
            {
                this.ETag = etag.Value.ToString("G");
            }

            if (metadata != null)
            {
                this.decodedMetadata = metadata.ToDictionary(m => m.Key, m => WebUtility.HtmlDecode(m.Value));
            }
            else
            {
                this.decodedMetadata = new Dictionary<string, string>();
            }
        }
    }
}
