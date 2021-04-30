namespace Proffer.Storage.Internal
{
    /// <summary>
    /// A simple reference of a stored file at a given path.
    /// </summary>
    /// <seealso cref="IPrivateFileReference" />
    public class PrivateFileReference : IPrivateFileReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateFileReference"/> class.
        /// </summary>
        /// <param name="path">The file path.</param>
        public PrivateFileReference(string path)
        {
            this.Path = path.Replace("\\", "/").TrimStart('/');
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string Path { get; }
    }
}
