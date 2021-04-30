namespace Providers.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when trying to create a file that already exists in a store.
    /// </summary>
    /// <seealso cref="Exception" />
    public class FileAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="filePath">The file path.</param>
        public FileAlreadyExistsException(string storeName, string filePath)
            : base($"The file {filePath} already exists in Store {storeName}.")
        {
        }
    }
}
