namespace Proffer.Storage.Azure.Internal
{
    using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

    /// <summary>
    /// Represents a file in a being-listed <see cref="AzureStore"/>.
    /// </summary>
    /// <seealso cref="FileInfoBase" />
    public class AzureListFileWrapper : FileInfoBase
    {
        private readonly AzureFileReference blob;
        private readonly string name;
        private readonly AzureListDirectoryWrapper parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureListFileWrapper"/> class.
        /// </summary>
        /// <param name="blob">The Azure Storage blob.</param>
        /// <param name="parent">The parent directory.</param>
        public AzureListFileWrapper(AzureFileReference blob, AzureListDirectoryWrapper parent)
        {
            this.blob = blob;
            int lastSlash = blob.Path.LastIndexOf('/');

            this.name = lastSlash >= 0 ? blob.Path.Substring(lastSlash + 1) : blob.Path;
            this.parent = parent;
        }

        /// <summary>
        /// A string containing the full path of the file.
        /// </summary>
        public override string FullName => this.blob.Path;

        /// <summary>
        /// A string containing the name of the file.
        /// </summary>
        public override string Name => this.name;

        /// <summary>
        /// The parent directory for the current file.
        /// </summary>
        public override DirectoryInfoBase ParentDirectory => this.parent;
    }
}
