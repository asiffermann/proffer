namespace Proffer.Storage.Azure.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using global::Azure.Storage.Sas;

    /// <summary>
    /// A reference of a stored file at a given path on Azure Storage.
    /// </summary>
    /// <seealso cref="IFileReference" />
    public class AzureFileReference : IFileReference
    {
        private readonly BlobClient blobClient;
        private Lazy<AzureFileProperties> propertiesLazy;
        private bool withMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileReference" /> class.
        /// </summary>
        /// <param name="blobClient">The Azure Storage blob client.</param>
        /// <param name="properties">The properties, if fetched.</param>
        public AzureFileReference(BlobClient blobClient, AzureFileProperties properties = null)
        {
            this.blobClient = blobClient;
            this.withMetadata = properties != null;

            this.Path = blobClient.Name;

            this.propertiesLazy = new Lazy<AzureFileProperties>(() =>
            {
                if (this.withMetadata)
                {
                    return properties;
                }

                throw new InvalidOperationException("Metadata are not loaded, please use withMetadata option");
            });
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
        public string PublicUrl => this.blobClient.Uri.ToString();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task DeleteAsync() => this.blobClient.DeleteAsync();

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
            await this.blobClient.DownloadToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// Updates the file content with the given <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The new file content.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task UpdateAsync(Stream stream) => this.blobClient.UploadAsync(stream, overwrite: true);

        /// <summary>
        /// Reads the file content into the given stream.
        /// </summary>
        /// <param name="targetStream">The target stream.</param>
        public async Task ReadToStreamAsync(Stream targetStream) => await this.blobClient.DownloadToAsync(targetStream);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public async ValueTask<string> ReadAllTextAsync()
        {
            using (var reader = new StreamReader(await this.blobClient.OpenReadAsync()))
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
            Uri sasUri = this.blobClient.GenerateSasUri(FromGenericToAzure(policy.Permissions), policy.ExpiryTime.GetValueOrDefault());

            return new ValueTask<string>(sasUri.ToString());
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

            Response<BlobProperties> refreshedProperties = await this.blobClient.GetPropertiesAsync();

            this.propertiesLazy = new Lazy<AzureFileProperties>(() => new AzureFileProperties(this.blobClient, refreshedProperties));
            this.withMetadata = true;
        }

        private static BlobSasPermissions FromGenericToAzure(SharedAccessPermissions permissions)
        {
            BlobSasPermissions result = 0;

            if (permissions.HasFlag(SharedAccessPermissions.Add))
            {
                result |= BlobSasPermissions.Add;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Create))
            {
                result |= BlobSasPermissions.Create;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Delete))
            {
                result |= BlobSasPermissions.Delete;
            }

            if (permissions.HasFlag(SharedAccessPermissions.List))
            {
                result |= BlobSasPermissions.List;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Read))
            {
                result |= BlobSasPermissions.Read;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Write))
            {
                result |= BlobSasPermissions.Write;
            }

            return result;
        }
    }
}
