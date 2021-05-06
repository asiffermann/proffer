namespace Proffer.Storage.Azure.Blobs.Internal
{
    using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

    /// <summary>
    /// Represents a file in a being-listed <see cref="AzureBlobsStore"/>.
    /// </summary>
    /// <seealso cref="FileInfoBase" />
    public class AzureBlobsListFileWrapper : FileInfoBase
    {
        private readonly AzureBlobsFileReference blob;
        private readonly string name;
        private readonly AzureBlobsListDirectoryWrapper parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobsListFileWrapper"/> class.
        /// </summary>
        /// <param name="file">The file reference.</param>
        /// <param name="parent">The parent directory.</param>
        public AzureBlobsListFileWrapper(AzureBlobsFileReference file, AzureBlobsListDirectoryWrapper parent)
        {
            this.blob = file;
            int lastSlash = file.Path.LastIndexOf('/');

            this.name = lastSlash >= 0 ? file.Path.Substring(lastSlash + 1) : file.Path;
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
