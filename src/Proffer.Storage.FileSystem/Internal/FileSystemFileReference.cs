namespace Proffer.Storage.FileSystem.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A reference of a stored file at a given path on a File System.
    /// </summary>
    /// <seealso cref="IFileReference" />
    public class FileSystemFileReference : IFileReference
    {
        private readonly FileSystemStore store;
        private readonly Lazy<string> publicUrlLazy;
        private readonly IExtendedPropertiesProvider extendedPropertiesProvider;
        private bool withMetadata;
        private Lazy<IFileProperties> propertiesLazy;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemFileReference"/> class.
        /// </summary>
        /// <param name="filePath">The file system path.</param>
        /// <param name="path">The path.</param>
        /// <param name="store">The store.</param>
        /// <param name="withMetadata">If set to <c>true</c>, the metadata for the file have been fetched.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <param name="publicUrlProvider">The public URL provider.</param>
        /// <param name="extendedPropertiesProvider">The extended properties provider.</param>
        public FileSystemFileReference(
            string filePath,
            string path,
            FileSystemStore store,
            bool withMetadata,
            FileExtendedProperties extendedProperties,
            IPublicUrlProvider publicUrlProvider,
            IExtendedPropertiesProvider extendedPropertiesProvider)
        {
            this.FileSystemPath = filePath;
            this.Path = path.Replace('\\', '/');
            this.store = store;
            this.extendedPropertiesProvider = extendedPropertiesProvider;
            this.withMetadata = withMetadata;

            this.propertiesLazy = new Lazy<IFileProperties>(() =>
            {
                if (withMetadata)
                {
                    return new FileSystemFileProperties(this.FileSystemPath, extendedProperties);
                }

                throw new InvalidOperationException("Metadata are not loaded, please use withMetadata option");
            });

            this.publicUrlLazy = new Lazy<string>(() =>
            {
                if (publicUrlProvider != null)
                {
                    return publicUrlProvider.GetPublicUrl(this.store.Name, this);
                }

                throw new InvalidOperationException("There is no FileSystemServer enabled.");
            });
        }

        /// <summary>
        /// Gets the file system path.
        /// </summary>
        public string FileSystemPath { get; }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the public URL.
        /// </summary>
        public string PublicUrl => this.publicUrlLazy.Value;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public IFileProperties Properties => this.propertiesLazy.Value;

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task DeleteAsync()
        {
            File.Delete(this.FileSystemPath);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="T:byte[]" /> containing the file content.
        /// </returns>
        public ValueTask<byte[]> ReadAllBytesAsync() => new(File.ReadAllBytes(this.FileSystemPath));

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public ValueTask<string> ReadAllTextAsync() => new(File.ReadAllText(this.FileSystemPath));

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream" /> containing the file content.
        /// </returns>
        public ValueTask<Stream> ReadAsync() => new(File.OpenRead(this.FileSystemPath));

        /// <summary>
        /// Reads the file content into the given stream.
        /// </summary>
        /// <param name="targetStream">The target stream.</param>
        public async Task ReadToStreamAsync(Stream targetStream)
        {
            using (FileStream file = File.Open(this.FileSystemPath, FileMode.Open, FileAccess.Read))
            {
                await file.CopyToAsync(targetStream);
            }
        }

        /// <summary>
        /// Updates the file content with the given <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The new file content.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task UpdateAsync(Stream stream)
        {
            using (FileStream file = File.Open(this.FileSystemPath, FileMode.Truncate, FileAccess.Write))
            {
                await stream.CopyToAsync(file);
            }
        }

        /// <summary>
        /// Saves the file properties.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SavePropertiesAsync()
        {
            if (this.extendedPropertiesProvider == null)
            {
                throw new InvalidOperationException("There is no FileSystem extended properties provider.");
            }

            return this.extendedPropertiesProvider.SaveExtendedPropertiesAsync(
                this.store.AbsolutePath,
                this,
                ( this.Properties as FileSystemFileProperties ).ExtendedProperties);
        }

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>A shared access signature to read file.</returns>
        public ValueTask<string> GetSharedAccessSignature(ISharedAccessPolicy policy) => throw new NotSupportedException();

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

            if (this.extendedPropertiesProvider == null)
            {
                throw new InvalidOperationException("There is no FileSystem extended properties provider.");
            }

            FileExtendedProperties extendedProperties = await this.extendedPropertiesProvider.GetExtendedPropertiesAsync(
                this.store.AbsolutePath,
                this);

            this.propertiesLazy = new Lazy<IFileProperties>(() => new FileSystemFileProperties(this.FileSystemPath, extendedProperties));
            this.withMetadata = true;
        }
    }
}
