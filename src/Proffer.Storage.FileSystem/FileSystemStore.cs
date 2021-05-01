namespace Proffer.Storage.FileSystem
{
    using Proffer.Storage.FileSystem.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A File System store allows to save, list or read files on a container in its configured <see cref="FileSystemStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IStore" />
    public class FileSystemStore : IStore
    {
        private readonly FileSystemStoreOptions storeOptions;
        private readonly IPublicUrlProvider publicUrlProvider;
        private readonly IExtendedPropertiesProvider extendedPropertiesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStore"/> class.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="publicUrlProvider">The public URL provider.</param>
        /// <param name="extendedPropertiesProvider">The extended properties provider.</param>
        public FileSystemStore(FileSystemStoreOptions storeOptions, IPublicUrlProvider publicUrlProvider, IExtendedPropertiesProvider extendedPropertiesProvider)
        {
            storeOptions.Validate();

            this.storeOptions = storeOptions;
            this.publicUrlProvider = publicUrlProvider;
            this.extendedPropertiesProvider = extendedPropertiesProvider;
        }

        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        public string Name => this.storeOptions.Name;

        /// <summary>
        /// Gets the absolute path.
        /// </summary>
        internal string AbsolutePath => this.storeOptions.AbsolutePath;

        /// <summary>
        /// Initializes the store by creating a container in its <see cref="IStorageProvider" />.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task InitAsync(CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(this.AbsolutePath))
            {
                Directory.CreateDirectory(this.AbsolutePath);
            }

            return Task.CompletedTask;
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
            string directoryPath = (string.IsNullOrEmpty(path) || path == "/" || path == "\\") ? this.AbsolutePath : Path.Combine(this.AbsolutePath, path);

            var result = new List<IFileReference>();
            if (Directory.Exists(directoryPath))
            {
                var allResultPaths = Directory.GetFiles(directoryPath)
                    .Select(fp => fp.Replace(this.AbsolutePath, "").Trim('/', '\\'))
                    .ToList();

                foreach (string resultPath in allResultPaths)
                {
                    result.Add(await this.InternalGetAsync(resultPath, withMetadata));
                }
            }

            return result.ToArray();
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
            string directoryPath = (string.IsNullOrEmpty(path) || path == "/" || path == "\\") ? this.AbsolutePath : Path.Combine(this.AbsolutePath, path);

            var result = new List<IFileReference>();
            if (Directory.Exists(directoryPath))
            {
                var matcher = new Microsoft.Extensions.FileSystemGlobbing.Matcher(StringComparison.Ordinal);
                matcher.AddInclude(searchPattern);

                Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult matches = matcher.Execute(
                    new Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoWrapper(new DirectoryInfo(directoryPath)));

                var allResultPaths = matches.Files
                    .Select(match => Path.Combine(path, match.Path).Trim('/', '\\'))
                    .ToList();

                foreach (string resultPath in allResultPaths)
                {
                    result.Add(await this.InternalGetAsync(resultPath, withMetadata));
                }
            }

            return result.ToArray();
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
        {
            if (uri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Cannot resolve an absolute URI with a FileSystem store.");
            }

            return await this.InternalGetAsync(uri.ToString(), withMetadata);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        public async Task DeleteAsync(IPrivateFileReference file)
        {
            Internal.FileSystemFileReference fileReference = await this.InternalGetAsync(file);
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
            Internal.FileSystemFileReference fileReference = await this.InternalGetAsync(file);
            return await fileReference.ReadAsync();
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
            Internal.FileSystemFileReference fileReference = await this.InternalGetAsync(file);
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
            Internal.FileSystemFileReference fileReference = await this.InternalGetAsync(file);
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
        public async ValueTask<IFileReference> SaveAsync(byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null)
        {
            using (var stream = new MemoryStream(data, 0, data.Length))
            {
                return await this.SaveAsync(stream, file, contentType, overwritePolicy);
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
            Internal.FileSystemFileReference fileReference = await this.InternalGetAsync(file, withMetadata: true, checkIfExists: false);
            bool fileExists = File.Exists(fileReference.FileSystemPath);

            if (fileExists)
            {
                if (overwritePolicy == OverwritePolicy.Never)
                {
                    throw new Exceptions.FileAlreadyExistsException(this.Name, file.Path);
                }
            }

            var properties = fileReference.Properties as Internal.FileSystemFileProperties;
            (string ETag, string ContentMD5) = ComputeHashes(data);

            if (!fileExists 
                || overwritePolicy == OverwritePolicy.Always
                || (overwritePolicy == OverwritePolicy.IfContentModified && properties.ContentMD5 != ContentMD5))
            {
                this.EnsurePathExists(fileReference.FileSystemPath);

                using (FileStream fileStream = File.Open(fileReference.FileSystemPath, FileMode.Create, FileAccess.Write))
                {
                    await data.CopyToAsync(fileStream);
                }
            }

            properties.ContentType = contentType;
            properties.ExtendedProperties.ETag = ETag;
            properties.ExtendedProperties.ContentMD5 = ContentMD5;

            if (metadata != null)
            {
                foreach (KeyValuePair<string, string> kvp in metadata)
                {
                    properties.Metadata.Add(kvp.Key, kvp.Value);
                }
            }

            await fileReference.SavePropertiesAsync();

            return fileReference;
        }

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// A shared access signature to read or list the store files.
        /// </returns>
        /// <exception cref="NotSupportedException"></exception>
        public ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy)
            => throw new NotSupportedException();

        private ValueTask<Internal.FileSystemFileReference> InternalGetAsync(IPrivateFileReference file, bool withMetadata = false, bool checkIfExists = true)
            => this.InternalGetAsync(file.Path, withMetadata, checkIfExists);

        private async ValueTask<Internal.FileSystemFileReference> InternalGetAsync(string path, bool withMetadata, bool checkIfExists = true)
        {
            string fullPath = Path.Combine(this.AbsolutePath, path);
            if (checkIfExists && !File.Exists(fullPath))
            {
                return null;
            }

            Internal.FileExtendedProperties extendedProperties = null;
            if (withMetadata)
            {
                if (this.extendedPropertiesProvider == null)
                {
                    throw new InvalidOperationException("There is no FileSystem extended properties provider.");
                }

                extendedProperties = await this.extendedPropertiesProvider.GetExtendedPropertiesAsync(
                    this.AbsolutePath,
                    new Storage.Internal.PrivateFileReference(path));
            }

            return new Internal.FileSystemFileReference(
                fullPath,
                path,
                this,
                withMetadata,
                extendedProperties,
                this.publicUrlProvider,
                this.extendedPropertiesProvider);
        }

        private void EnsurePathExists(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private static (string ETag, string ContentMD5) ComputeHashes(Stream stream)
        {
            string eTag = string.Empty;
            string contentMD5 = string.Empty;

            stream.Seek(0, SeekOrigin.Begin);
            using (var md5 = MD5.Create())
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] hash = md5.ComputeHash(stream);
                stream.Seek(0, SeekOrigin.Begin);
                contentMD5 = Convert.ToBase64String(hash);
                string hex = BitConverter.ToString(hash);
                eTag = $"\"{hex.Replace("-", "")}\"";
            }

            return (eTag, contentMD5);
        }
    }
}
