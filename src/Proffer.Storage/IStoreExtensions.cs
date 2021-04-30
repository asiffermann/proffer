namespace Proffer.Storage
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// <see cref="IStore"/> extension methods.
    /// </summary>
    public static class IStoreExtensions
    {
        /// <summary>
        /// Lists the files under <paramref name="path" />.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The path.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> list under <paramref name="path" />.
        /// </returns>
        public static ValueTask<IFileReference[]> ListAsync(this IStore store, string path, bool recursive = false, bool withMetadata = false)
            => store.ListAsync(path, recursive: recursive, withMetadata: withMetadata);

        /// <summary>
        /// Lists the files under <paramref name="path" /> matching the <paramref name="searchPattern" />.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="recursive">If set to <c>true</c>, recurse the listing across folders.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for each file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> list under <paramref name="path" /> matching the <paramref name="searchPattern" />.
        /// </returns>
        public static ValueTask<IFileReference[]> ListAsync(this IStore store, string path, string searchPattern, bool recursive = false, bool withMetadata = false)
            => store.ListAsync(path, searchPattern, recursive: recursive, withMetadata: withMetadata);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The file path.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public static Task DeleteAsync(this IStore store, string path)
            => store.DeleteAsync(new Internal.PrivateFileReference(path));

        /// <summary>
        /// Gets the file reference from path.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The file path.</param>
        /// <param name="withMetadata">If set to <c>true</c>, fetch metadata for the file.</param>
        /// <returns>
        /// The <see cref="IFileReference" /> at path.
        /// </returns>
        public static ValueTask<IFileReference> GetAsync(this IStore store, string path, bool withMetadata = false)
            => store.GetAsync(new Internal.PrivateFileReference(path), withMetadata: withMetadata);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The file path.</param>
        /// <returns>
        /// A <see cref="Stream" /> containing the file content.
        /// </returns>
        public static ValueTask<Stream> ReadAsync(this IStore store, string path)
            => store.ReadAsync(new Internal.PrivateFileReference(path));

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The file path.</param>
        /// <returns>
        /// A <see cref="T:byte[]" /> containing the file content.
        /// </returns>
        public static ValueTask<byte[]> ReadAllBytesAsync(this IStore store, string path)
            => store.ReadAllBytesAsync(new Internal.PrivateFileReference(path));

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="path">The file path.</param>
        /// <returns>
        /// A <see cref="string" /> containing the file content.
        /// </returns>
        public static ValueTask<string> ReadAllTextAsync(this IStore store, string path)
            => store.ReadAllTextAsync(new Internal.PrivateFileReference(path));

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="data">The file content.</param>
        /// <param name="path">The file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The saved <see cref="IFileReference" />.
        /// </returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        public static ValueTask<IFileReference> SaveAsync(this IStore store, byte[] data, string path, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string,string> metadata = null)
            => store.SaveAsync(data, new Internal.PrivateFileReference(path), contentType, overwritePolicy, metadata);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="data">The file content.</param>
        /// <param name="path">The file path.</param>
        /// <param name="contentType">The content-type of the file.</param>
        /// <param name="overwritePolicy">The overwrite policy.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The saved <see cref="IFileReference" />.
        /// </returns>
        /// <exception cref="Exceptions.FileAlreadyExistsException"></exception>
        public static ValueTask<IFileReference> SaveAsync(this IStore store, Stream data, string path, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string,string> metadata = null)
            => store.SaveAsync(data, new Internal.PrivateFileReference(path), contentType, overwritePolicy, metadata);
    }
}
