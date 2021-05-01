namespace Proffer.Storage.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using global::Azure.Storage.Sas;
    using Proffer.Storage.Azure.Configuration;

    /// <summary>
    /// An Azure store allows to save, list or read files on a container in its configured <see cref="AzureStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IStore" />
    public class AzureStore : IStore
    {
        private readonly AzureStoreOptions storeOptions;
        private readonly BlobContainerClient container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStore"/> class.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        public AzureStore(AzureStoreOptions storeOptions)
        {
            storeOptions.Validate();

            this.storeOptions = storeOptions;
            this.container = new BlobContainerClient(storeOptions.ConnectionString, storeOptions.FolderName);
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
        public Task InitAsync(CancellationToken cancellationToken = default)
        {
            PublicAccessType accessType = this.storeOptions.AccessLevel switch
            {
                Storage.Configuration.AccessLevel.Public => PublicAccessType.BlobContainer,
                Storage.Configuration.AccessLevel.Confidential => PublicAccessType.Blob,
                _ => PublicAccessType.None,
            };

            return this.container.CreateIfNotExistsAsync(accessType, cancellationToken: cancellationToken);
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
            List<BlobItem> results = await this.GetBlobItems(CleanPath(path), recursive, withMetadata);

            return results
                .Select(blobItem =>
                {
                    BlobClient blobClient = this.container.GetBlobClient(blobItem.Name);
                    return new Internal.AzureFileReference(blobClient, new Internal.AzureFileProperties(blobClient, blobItem));
                })
                .ToArray();
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
            path = CleanPath(path);

            string prefix = path;
            int firstWildCard = searchPattern.IndexOf('*');
            if (firstWildCard >= 0)
            {
                prefix += searchPattern.Substring(0, firstWildCard);
                searchPattern = searchPattern.Substring(firstWildCard);
            }

            var matcher = new Microsoft.Extensions.FileSystemGlobbing.Matcher(StringComparison.Ordinal);
            matcher.AddInclude(searchPattern);

            List<BlobItem> results = await this.GetBlobItems(prefix, recursive, withMetadata);

            var pathMap = results
                .Select(blobItem =>
                {
                    BlobClient blobClient = this.container.GetBlobClient(blobItem.Name);
                    return new Internal.AzureFileReference(blobClient, new Internal.AzureFileProperties(blobClient, blobItem));
                })
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
        public async ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata)
            => await this.InternalGetAsync(file, withMetadata);

        /// <summary>
        /// Gets the file reference from URI.
        /// </summary>
        /// <param name="uri">The file uniform resource identifier (URI).</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async ValueTask<IFileReference> GetAsync(Uri uri, bool withMetadata)
            => await this.InternalGetAsync(uri, withMetadata);

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
            using (var stream = new MemoryStream(data, 0, data.Length))
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
            BlobClient blobClient = this.container.GetBlobClient(file.Path);
            bool blobExists = await blobClient.ExistsAsync();

            bool uploadBlob = true;
            if (blobExists)
            {
                switch (overwritePolicy)
                {
                    case OverwritePolicy.Always:
                        uploadBlob = true;
                        break;

                    case OverwritePolicy.IfContentModified:
                        Response<BlobProperties> properties = await blobClient.GetPropertiesAsync();
                        using (var md5 = MD5.Create())
                        {
                            data.Seek(0, SeekOrigin.Begin);
                            string contentMD5 = Convert.ToBase64String(md5.ComputeHash(data));
                            data.Seek(0, SeekOrigin.Begin);

                            uploadBlob = contentMD5 != Convert.ToBase64String(properties.Value.ContentHash);
                        }

                        break;

                    case OverwritePolicy.Never:
                    default:
                        throw new Exceptions.FileAlreadyExistsException(this.Name, file.Path);
                }
            }

            if (uploadBlob)
            {
                var blobUploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType
                    }
                };

                if (metadata != null)
                {
                    blobUploadOptions.Metadata = metadata;
                }

                await blobClient.UploadAsync(data, blobUploadOptions);
            }

            Response<BlobProperties> refreshedProperties = await blobClient.GetPropertiesAsync();

            return new Internal.AzureFileReference(blobClient, new Internal.AzureFileProperties(blobClient, refreshedProperties));
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
            Uri sasUri = this.container.GenerateSasUri(FromGenericToAzure(policy.Permissions), policy.ExpiryTime.GetValueOrDefault());

            return new ValueTask<string>(sasUri.Query);
        }

        private static BlobContainerSasPermissions FromGenericToAzure(SharedAccessPermissions permissions)
        {
            BlobContainerSasPermissions result = 0;

            if (permissions.HasFlag(SharedAccessPermissions.Add))
            {
                result |= BlobContainerSasPermissions.Add;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Create))
            {
                result |= BlobContainerSasPermissions.Create;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Delete))
            {
                result |= BlobContainerSasPermissions.Delete;
            }

            if (permissions.HasFlag(SharedAccessPermissions.List))
            {
                result |= BlobContainerSasPermissions.List;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Read))
            {
                result |= BlobContainerSasPermissions.Read;
            }

            if (permissions.HasFlag(SharedAccessPermissions.Write))
            {
                result |= BlobContainerSasPermissions.Write;
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
                string blobName = uri.IsAbsoluteUri ? this.container.Uri.MakeRelativeUri(uri).ToString() : uri.ToString();
                BlobClient blobClient = this.container.GetBlobClient(blobName);

                if (!await blobClient.ExistsAsync())
                {
                    return null;
                }

                Internal.AzureFileProperties properties = null;
                if (withMetadata)
                {
                    Response<BlobProperties> blobProperties = await blobClient.GetPropertiesAsync();
                    properties = new Internal.AzureFileProperties(blobClient, blobProperties);
                }

                return new Internal.AzureFileReference(blobClient, properties);
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return null;
            }
        }

        private async Task<List<BlobItem>> GetBlobItems(string path, bool recursive, bool withMetadata)
        {
            AsyncPageable<BlobHierarchyItem> hierarchy = this.container.GetBlobsByHierarchyAsync(
                withMetadata ? BlobTraits.Metadata : BlobTraits.None,
                BlobStates.None,
                "/",
                path);

            var results = new List<BlobItem>();
            await foreach (BlobHierarchyItem blobOrFolder in hierarchy)
            {
                if (blobOrFolder.IsBlob)
                {
                    results.Add(blobOrFolder.Blob);
                }
                else if (blobOrFolder.IsPrefix && recursive)
                {
                    results.AddRange(await this.GetBlobItems(blobOrFolder.Prefix, recursive, withMetadata));
                }
            }

            return results;
        }

        private static string CleanPath(string path)
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

            return path;
        }
    }
}
