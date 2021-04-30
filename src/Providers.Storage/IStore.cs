namespace Providers.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A store allows to save, list or read files on a container in its configured <see cref="IStorageProvider"/>.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initializes the store by creating a container in its <see cref="IStorageProvider"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InitAsync();

        /// <summary>
        /// Lists the files under <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>The <see cref="IFileReference"/> list under <paramref name="path"/>.</returns>
        ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata);

        /// <summary>
        /// Lists the files under <paramref name="path"/> matching the <paramref name="searchPattern"/>.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>The <see cref="IFileReference"/> list under <paramref name="path"/> matching the <paramref name="searchPattern"/>.</returns>
        ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata);

        /// <summary>
        /// Gets the file reference from path.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>The <see cref="IFileReference"/> at path.</returns>
        ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata);

        /// <summary>
        /// Gets the file reference from URI.
        /// </summary>
        /// <param name="uri">The file uniform resource identifier (URI).</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>The <see cref="IFileReference"/> at path.</returns>
        ValueTask<IFileReference> GetAsync(Uri uri, bool withMetadata);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(IPrivateFileReference file);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A <see cref="Stream"/> containing the file content.</returns>
        ValueTask<Stream> ReadAsync(IPrivateFileReference file);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A <see cref="T:byte[]"/> containing the file content.</returns>
        ValueTask<byte[]> ReadAllBytesAsync(IPrivateFileReference file);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A <see cref="string"/> containing the file content.</returns>
        ValueTask<string> ReadAllTextAsync(IPrivateFileReference file);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="data">The file content.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The saved <see cref="IFileReference"/>.</returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        ValueTask<IFileReference> SaveAsync(byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="data">The file content.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The saved <see cref="IFileReference"/>.</returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null);

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>A shared access signature to read or list the store files.</returns>
        ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy);
    }
}
