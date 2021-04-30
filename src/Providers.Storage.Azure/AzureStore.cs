namespace Providers.Storage.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Core;
    using Providers.Storage.Azure.Configuration;

    /// <summary>
    /// An Azure store allows to save, list or read files on a container in its configured <see cref="AzureStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IStore" />
    public class AzureStore : IStore
    {
        private readonly AzureStoreOptions storeOptions;
        private readonly Lazy<CloudBlobClient> client;
        private readonly Lazy<CloudBlobContainer> container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStore"/> class.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        public AzureStore(AzureStoreOptions storeOptions)
        {
            storeOptions.Validate();

            this.storeOptions = storeOptions;
            this.client = new Lazy<CloudBlobClient>(() => CloudStorageAccount.Parse(storeOptions.ConnectionString).CreateCloudBlobClient());
            this.container = new Lazy<CloudBlobContainer>(() => this.client.Value.GetContainerReference(storeOptions.FolderName));
        }

        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        public string Name => this.storeOptions.Name;

        /// <summary>
        /// Initializes the store by creating a container in its <see cref="IStorageProvider" />.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task InitAsync()
        {
            BlobContainerPublicAccessType accessType = this.storeOptions.AccessLevel switch
            {
                Storage.Configuration.AccessLevel.Public => BlobContainerPublicAccessType.Container,
                Storage.Configuration.AccessLevel.Confidential => BlobContainerPublicAccessType.Blob,
                _ => BlobContainerPublicAccessType.Off,
            };

            return this.container.Value.CreateIfNotExistsAsync(accessType, null, null);
        }

        /// <summary>
        /// Lists the files under <paramref name="path" />.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> list under <paramref name="path" />.
        /// </returns>
        public async ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = null;
            }
            else
            {
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
            }

            BlobContinuationToken continuationToken = null;
            var results = new List<IListBlobItem>();

            do
            {
                BlobResultSegment response = await this.container.Value.ListBlobsSegmentedAsync(
                    path,
                    recursive,
                    withMetadata ? BlobListingDetails.Metadata : BlobListingDetails.None,
                    null,
                    continuationToken,
                    new BlobRequestOptions(),
                    new OperationContext());

                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            }
            while (continuationToken != null);

            return results.OfType<ICloudBlob>().Select(blob => new Internal.AzureFileReference(blob, withMetadata: withMetadata)).ToArray();
        }

        /// <summary>
        /// Lists the files under <paramref name="path" /> matching the <paramref name="searchPattern" />.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> list under <paramref name="path" /> matching the <paramref name="searchPattern" />.
        /// </returns>
        public async ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = null;
            }
            else
            {
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
            }

            string prefix = path;
            int firstWildCard = searchPattern.IndexOf('*');
            if (firstWildCard >= 0)
            {
                prefix += searchPattern.Substring(0, firstWildCard);
                searchPattern = searchPattern.Substring(firstWildCard);
            }

            var matcher = new Microsoft.Extensions.FileSystemGlobbing.Matcher(StringComparison.Ordinal);
            matcher.AddInclude(searchPattern);

            var operationContext = new OperationContext();
            BlobContinuationToken continuationToken = null;
            var results = new List<IListBlobItem>();

            do
            {
                BlobResultSegment response = await this.container.Value.ListBlobsSegmentedAsync(
                    prefix,
                    recursive,
                    withMetadata ? BlobListingDetails.Metadata : BlobListingDetails.None,
                    null,
                    continuationToken,
                    new BlobRequestOptions(),
                    new OperationContext());

                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            }
            while (continuationToken != null);

            var pathMap = results.OfType<ICloudBlob>()
                .Select(blob => new Internal.AzureFileReference(blob, withMetadata: withMetadata))
                .ToDictionary(x => Path.GetFileName(x.Path));

            Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult filteredResults = matcher.Execute(new Internal.AzureListDirectoryWrapper(path, pathMap));

            return filteredResults.Files.Select(x => pathMap[x.Path]).ToArray();
        }

        /// <summary>
        /// Gets the file reference from path.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        public async ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata) => await this.InternalGetAsync(file, withMetadata);

        /// <summary>
        /// Gets the file reference from URI.
        /// </summary>
        /// <param name="uri">The file uniform resource identifier (URI).</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        public async ValueTask<IFileReference> GetAsync(Uri uri, bool withMetadata) => await this.InternalGetAsync(uri, withMetadata);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        public async Task DeleteAsync(IPrivateFileReference file)
        {
            Internal.AzureFileReference fileReference = await this.InternalGetAsync(file);
            await fileReference.DeleteAsync();
        }

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="Stream" /> containing the file content.
        /// </returns>
        public async ValueTask<Stream> ReadAsync(IPrivateFileReference file)
        {
            Internal.AzureFileReference fileReference = await this.InternalGetAsync(file);
            return await fileReference.ReadInMemoryAsync();
        }

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="T:byte[]" /> containing the file content.
        /// </returns>
        public async ValueTask<byte[]> ReadAllBytesAsync(IPrivateFileReference file)
        {
            Internal.AzureFileReference fileReference = await this.InternalGetAsync(file);
            return await fileReference.ReadAllBytesAsync();
        }

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public async ValueTask<string> ReadAllTextAsync(IPrivateFileReference file)
        {
            Internal.AzureFileReference fileReference = await this.InternalGetAsync(file);
            return await fileReference.ReadAllTextAsync();
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="data">The file content.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The saved <see cref="IFileReference" />.
        /// </returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        public async ValueTask<IFileReference> SaveAsync(byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null)
        {
            using (var stream = new SyncMemoryStream(data, 0, data.Length))
            {
                return await this.SaveAsync(stream, file, contentType, overwritePolicy, metadata);
            }
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="data">The file content.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The saved <see cref="IFileReference" />.
        /// </returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        public async ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null)
        {
            bool uploadBlob = true;
            CloudBlockBlob blockBlob = this.container.Value.GetBlockBlobReference(file.Path);
            bool blobExists = await blockBlob.ExistsAsync();

            if (blobExists)
            {
                if (overwritePolicy == OverwritePolicy.Never)
                {
                    throw new Exceptions.FileAlreadyExistsException(this.Name, file.Path);
                }

                await blockBlob.FetchAttributesAsync();

                if (overwritePolicy == OverwritePolicy.IfContentModified)
                {
                    using (var md5 = MD5.Create())
                    {
                        data.Seek(0, SeekOrigin.Begin);
                        string contentMD5 = Convert.ToBase64String(md5.ComputeHash(data));
                        data.Seek(0, SeekOrigin.Begin);
                        uploadBlob = contentMD5 != blockBlob.Properties.ContentMD5;
                    }
                }
            }

            if (metadata != null)
            {
                foreach (KeyValuePair<string, string> kvp in metadata)
                {
                    blockBlob.Metadata.Add(kvp.Key, kvp.Value);
                }
            }

            if (uploadBlob)
            {
                await blockBlob.UploadFromStreamAsync(data);
            }

            var reference = new Internal.AzureFileReference(blockBlob, withMetadata: true);

            if (reference.Properties.ContentType != contentType)
            {
                reference.Properties.ContentType = contentType;
                await reference.SavePropertiesAsync();
            }

            return reference;
        }

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// A shared access signature to read or list the store files.
        /// </returns>
        public ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy)
        {
            var adHocPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = policy.StartTime,
                SharedAccessExpiryTime = policy.ExpiryTime,
                Permissions = FromGenericToAzure(policy.Permissions),
            };

            return new ValueTask<string>(this.container.Value.GetSharedAccessSignature(adHocPolicy));
        }

        internal static SharedAccessBlobPermissions FromGenericToAzure(SharedAccessPermissions permissions)
        {
            SharedAccessBlobPermissions result = SharedAccessBlobPermissions.None;

            if (permissions.HasFlag(SharedAccessPermissions.Add))
            {
                result |= SharedAccessBlobPermissions.Add;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Create))
            {
                result |= SharedAccessBlobPermissions.Create;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Delete))
            {
                result |= SharedAccessBlobPermissions.Delete;
            }

            if (permissions.HasFlag(SharedAccessPermissions.List))
            {
                result |= SharedAccessBlobPermissions.List;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Read))
            {
                result |= SharedAccessBlobPermissions.Read;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Write))
            {
                result |= SharedAccessBlobPermissions.Write;
            }

            return result;
        }

        private ValueTask<Internal.AzureFileReference> InternalGetAsync(IPrivateFileReference file, bool withMetadata = false)
        {
            return this.InternalGetAsync(new Uri(file.Path, UriKind.Relative), withMetadata);
        }

        private async ValueTask<Internal.AzureFileReference> InternalGetAsync(Uri uri, bool withMetadata)
        {
            try
            {
                ICloudBlob blob;

                if (uri.IsAbsoluteUri)
                {
                    // When the URI is absolute, we cannot get a simple reference to the blob, so the
                    // properties and metadata are fetched, even if it was not asked.

                    blob = await this.client.Value.GetBlobReferenceFromServerAsync(uri);
                    withMetadata = true;
                }
                else
                {
                    if (withMetadata)
                    {
                        blob = await this.container.Value.GetBlobReferenceFromServerAsync(uri.ToString());
                    }
                    else
                    {
                        blob = this.container.Value.GetBlockBlobReference(uri.ToString());
                        if (!await blob.ExistsAsync())
                        {
                            return null;
                        }
                    }
                }

                return new Internal.AzureFileReference(blob, withMetadata);
            }
            catch (StorageException storageException)
            {
                if (storageException.RequestInformation.HttpStatusCode == 404)
                {
                    return null;
                }

                throw;
            }
        }
    }
}
