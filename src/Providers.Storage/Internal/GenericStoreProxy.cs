namespace Providers.Storage.Internal
{
    using Configuration;
    using Dawn;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic <see cref="IStore"/> proxy to allow direct dependency injection of a <see cref="IStore{TOptions}"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of the store options.</typeparam>
    /// <seealso cref="IStore" />
    /// <seealso cref="IStore{TOptions}" />
    public class GenericStoreProxy<TOptions> : IStore, IStore<TOptions>
        where TOptions : class, IStoreOptions, new()
    {
        private readonly IStore innerStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericStoreProxy{TOptions}"/> class.
        /// </summary>
        /// <param name="factory">The storage factory.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GenericStoreProxy(IStorageFactory factory, IOptions<TOptions> options)
        {
            Guard.Argument(options, nameof(options)).NotNull("Unable to build generic Store. Did you forget to configure your options?");

            this.innerStore = factory.GetStore(options.Value.Name, options.Value);
        }

        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        public string Name => this.innerStore.Name;

        /// <summary>
        /// Initializes the store by creating a container in its <see cref="IStorageProvider" />.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task InitAsync() => this.innerStore.InitAsync();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task DeleteAsync(IPrivateFileReference file) => this.innerStore.DeleteAsync(file);

        /// <summary>
        /// Gets the file reference from URI.
        /// </summary>
        /// <param name="file">The file uniform resource identifier (URI).</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        public ValueTask<IFileReference> GetAsync(Uri file, bool withMetadata) => this.innerStore.GetAsync(file, withMetadata);

        /// <summary>
        /// Gets the file reference from path.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        public ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata) => this.innerStore.GetAsync(file, withMetadata);

        /// <summary>
        /// Lists the files under <paramref name="path" />.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> list under <paramref name="path" />.
        /// </returns>
        public ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata) => this.innerStore.ListAsync(path, recursive, withMetadata);

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
        public ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata) => this.innerStore.ListAsync(path, searchPattern, recursive, withMetadata);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="T:byte[]" /> containing the file content.
        /// </returns>
        public ValueTask<byte[]> ReadAllBytesAsync(IPrivateFileReference file) => this.innerStore.ReadAllBytesAsync(file);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public ValueTask<string> ReadAllTextAsync(IPrivateFileReference file) => this.innerStore.ReadAllTextAsync(file);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A <see cref="Stream" /> containing the file content.
        /// </returns>
        public ValueTask<Stream> ReadAsync(IPrivateFileReference file) => this.innerStore.ReadAsync(file);

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
        public ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null) => this.innerStore.SaveAsync(data, file, contentType, overwritePolicy);

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
        public ValueTask<IFileReference> SaveAsync(byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null) => this.innerStore.SaveAsync(data, file, contentType, overwritePolicy);

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>
        /// A shared access signature to read or list the store files.
        /// </returns>
        public ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy) => this.innerStore.GetSharedAccessSignatureAsync(policy);
    }
}
