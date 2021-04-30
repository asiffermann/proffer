namespace Proffer.Storage.FileSystem
{
    /// <summary>
    /// Provides a way to serve files from an HTTP URL on a File System.
    /// </summary>
    public interface IPublicUrlProvider
    {
        /// <summary>
        /// Gets the public URL of a file reference.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>The public URL.</returns>
        string GetPublicUrl(string storeName, Internal.FileSystemFileReference file);
    }
}
