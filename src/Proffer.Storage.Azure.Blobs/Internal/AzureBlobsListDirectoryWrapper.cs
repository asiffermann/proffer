namespace Proffer.Storage.Azure.Blobs.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

    /// <summary>
    /// Represents a directory in a being-listed <see cref="AzureBlobsStore"/>.
    /// </summary>
    /// <seealso cref="DirectoryInfoBase" />
    public class AzureBlobsListDirectoryWrapper : DirectoryInfoBase
    {
        private readonly string name;
        private readonly string fullName;
        private readonly string path;
        private readonly Dictionary<string, AzureBlobsFileReference> files;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobsListDirectoryWrapper"/> class.
        /// </summary>
        /// <param name="childrens">The childrens.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "FileSystemGlobbing abstractions")]
        public AzureBlobsListDirectoryWrapper(FileSystemInfoBase childrens)
        {
            this.fullName = "root";
            this.ParentDirectory = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobsListDirectoryWrapper"/> class.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <param name="files">The files.</param>
        public AzureBlobsListDirectoryWrapper(string path, Dictionary<string, AzureBlobsFileReference> files)
        {
            this.path = path ?? "";
            this.files = files;
            this.fullName = this.path;
            int lastSlash = this.path.LastIndexOf('/');

            this.name = lastSlash >= 0 ? path.Substring(lastSlash + 1) : path;
        }

        /// <summary>
        /// A string containing the full path of the directory.
        /// </summary>
        public override string FullName => this.fullName;

        /// <summary>
        /// A string containing the name of the directory.
        /// </summary>
        public override string Name => this.name;

        /// <summary>
        /// The parent directory for the current directory.
        /// </summary>
        public override DirectoryInfoBase ParentDirectory { get; }

        /// <summary>
        /// Enumerates all files and directories in the directory.
        /// </summary>
        /// <returns>
        /// Collection of files and directories
        /// </returns>
        public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos()
            => this.files.Values.Select(file => new AzureBlobsListFileWrapper(file, this));

        /// <summary>
        /// Returns an instance of <see cref="DirectoryInfoBase" /> that represents a subdirectory.
        /// </summary>
        /// <param name="path">The directory name</param>
        /// <returns>
        /// Instance of <see cref="DirectoryInfoBase" /> even if directory does not exist
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DirectoryInfoBase GetDirectory(string path) => throw new NotImplementedException();

        /// <summary>
        /// Returns an instance of <see cref="FileInfoBase" /> that represents a file in the directory.
        /// </summary>
        /// <param name="path">The file name</param>
        /// <returns>
        /// Instance of <see cref="FileInfoBase" /> even if file does not exist
        /// </returns>
        public override FileInfoBase GetFile(string path) => new AzureBlobsListFileWrapper(this.files[path], this);
    }
}
