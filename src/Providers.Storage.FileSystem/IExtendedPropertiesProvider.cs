namespace Providers.Storage.FileSystem
{
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a way to store and retrieve extended file properties to match the requirements of <see cref="IFileProperties"/> on a File System.
    /// </summary>
    public interface IExtendedPropertiesProvider
    {
        /// <summary>
        /// Gets the extended properties of a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A loaded <see cref="Internal.FileExtendedProperties"/> instance or a default one if not found.</returns>
        ValueTask<Internal.FileExtendedProperties> GetExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file);

        /// <summary>
        /// Saves the extended properties for a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file, Internal.FileExtendedProperties extendedProperties);
    }
}
