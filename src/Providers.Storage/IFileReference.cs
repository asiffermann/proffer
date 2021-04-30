namespace Providers.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A reference of a stored file at a given path.
    /// </summary>
    /// <seealso cref="IPrivateFileReference" />
    public interface IFileReference : IPrivateFileReference
    {
        /// <summary>
        /// Gets the public URL.
        /// </summary>
        string PublicUrl { get; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        IFileProperties Properties { get; }

        /// <summary>
        /// Reads the file content into the given stream.
        /// </summary>
        /// <param name="targetStream">The target stream.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ReadToStreamAsync(Stream targetStream);

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>A <see cref="Stream"/> containing the file content.</returns>
        ValueTask<Stream> ReadAsync();

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the file content.</returns>
        ValueTask<string> ReadAllTextAsync();

        /// <summary>
        /// Reads the file content.
        /// </summary>
        /// <returns>A <see cref="T:byte[]"/> containing the file content.</returns>
        ValueTask<byte[]> ReadAllBytesAsync();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync();

        /// <summary>
        /// Updates the file content with the given <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The new file content.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Stream stream);

        /// <summary>
        /// Saves the file properties.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SavePropertiesAsync();

        /// <summary>
        /// Gets a shared access signature.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>A shared access signature to read file.</returns>
        ValueTask<string> GetSharedAccessSignature(ISharedAccessPolicy policy);

        /// <summary>
        /// Fetches the file properties.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task FetchProperties();
    }
}
