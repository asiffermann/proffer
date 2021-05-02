namespace Proffer.Storage.FileSystem
{
    using System;
    using System.Threading.Tasks;
    using Proffer.Storage;
    using Proffer.Storage.FileSystem.Internal;

    /// <summary>
    /// Default <see cref="IExtendedPropertiesProvider"/> without property storage capacity.
    /// </summary>
    public class NoExtendedPropertiesProvider : IExtendedPropertiesProvider
    {
        /// <summary>
        /// Gets the extended properties of a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>A loaded <see cref="Internal.FileExtendedProperties"/> instance or a default one if not found.</returns>
        public ValueTask<FileExtendedProperties> GetExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file)
            => new(Task.FromResult(new FileExtendedProperties()));

        /// <summary>
        /// Saves the extended properties for a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task SaveExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file, FileExtendedProperties extendedProperties)
            => throw new NotSupportedException("Please register a IExtendedPropertiesProvider to store properties and metadata on a FileSystem.");
    }
}
