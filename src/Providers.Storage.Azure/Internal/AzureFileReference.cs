namespace Providers.Storage.Azure.Internal
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A reference of a stored file at a given path on Azure Storage.
    /// </summary>
    /// <seealso cref="IFileReference" />
    public class AzureFileReference : IFileReference
    {
        private Lazy<AzureFileProperties> propertiesLazy;
        private bool withMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileReference"/> class.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="cloudBlob">The Azure Storage blob.</param>
        /// <param name="withMetadata">If set to <c>true</c>, the metadata for the file have been fetched.</param>
        public AzureFileReference(string path, ICloudBlob cloudBlob, bool withMetadata)
        {
            this.Path = path;
            this.CloudBlob = cloudBlob;
            this.withMetadata = withMetadata;
            this.propertiesLazy = new Lazy<AzureFileProperties>(() =>
            {
                if (withMetadata && cloudBlob.Metadata != null && cloudBlob.Properties != null)
                {
                    return new AzureFileProperties(cloudBlob);
                }

                throw new InvalidOperationException("Metadata are not loaded, please use withMetadata option");
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileReference"/> class.
        /// </summary>
        /// <param name="cloudBlob">The Azure Storage blob.</param>
        /// <param name="withMetadata">If set to <c>true</c>, the metadata for the file have been fetched.</param>
        public AzureFileReference(ICloudBlob cloudBlob, bool withMetadata)
            : this(cloudBlob.Name, cloudBlob, withMetadata)
        {
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public IFileProperties Properties => this.propertiesLazy.Value;

        /// <summary>
        /// Gets the public URL.
        /// </summary>
        public string PublicUrl => this.CloudBlob.Uri.ToString();

        /// <summary>
        /// Gets the Azure Storage blob.
        /// </summary>
        public ICloudBlob CloudBlob { get; }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task DeleteAsync() => this.CloudBlob.DeleteAsync();

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream" /> containing the file content.
        /// </returns>
        public async ValueTask<Stream> ReadAsync() => await this.ReadInMemoryAsync();

        /// <summary>
        /// Reads the file content in memory.
        /// </summary>
        /// <returns>A new <see cref="MemoryStream" /> containing the file content.</returns>
        public async ValueTask<MemoryStream> ReadInMemoryAsync()
        {
            var memoryStream = new MemoryStream();
            await this.CloudBlob.DownloadRangeToStreamAsync(memoryStream, null, null);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// Updates the file content with the given <see cref="Stream" />.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task UpdateAsync(Stream stream) => this.CloudBlob.UploadFromStreamAsync(stream);

        /// <summary>
        /// Reads the file content into the given stream.
        /// </summary>
        /// <param name="targetStream">The target stream.</param>
        public async Task ReadToStreamAsync(Stream targetStream) => await this.CloudBlob.DownloadRangeToStreamAsync(targetStream, null, null);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public async ValueTask<string> ReadAllTextAsync()
        {
            using (var reader = new StreamReader(await this.CloudBlob.OpenReadAsync(AccessCondition.GenerateEmptyCondition(), new BlobRequestOptions(), new OperationContext())))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="T:byte[]" /> containing the file content.
        /// </returns>
        public async ValueTask<byte[]> ReadAllBytesAsync() => ( await this.ReadInMemoryAsync() ).ToArray();

        /// <summary>
        /// Saves the file properties.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SavePropertiesAsync() => this.propertiesLazy.Value.SaveAsync();

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// A shared access signature to read file.
        /// </returns>
        public ValueTask<string> GetSharedAccessSignature(ISharedAccessPolicy policy)
        {
            var adHocPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = policy.StartTime,
                SharedAccessExpiryTime = policy.ExpiryTime,
                Permissions = AzureStore.FromGenericToAzure(policy.Permissions),
            };

            return new ValueTask<string>(this.CloudBlob.GetSharedAccessSignature(adHocPolicy));
        }

        /// <summary>
        /// Fetches the file properties.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task FetchProperties()
        {
            if (this.withMetadata)
            {
                return;
            }

            await this.CloudBlob.FetchAttributesAsync();

            this.propertiesLazy = new Lazy<AzureFileProperties>(() => new AzureFileProperties(this.CloudBlob));
            this.withMetadata = true;
        }
    }
}
