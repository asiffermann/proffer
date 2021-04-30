namespace Providers.Storage.FileSystem.ExtendedProperties.FileSystem.Internal
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Storage.FileSystem.Internal;

    /// <summary>
    /// Provides a way to store and retrieve extended file properties to match the requirements of <see cref="IFileProperties"/> on a File System using JSON files.
    /// </summary>
    public class ExtendedPropertiesProvider : IExtendedPropertiesProvider
    {
        private readonly FileSystemExtendedPropertiesOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPropertiesProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ExtendedPropertiesProvider(IOptions<FileSystemExtendedPropertiesOptions> options)
        {
            this.options = options.Value;
        }

        /// <summary>
        /// Gets the extended properties of a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <returns>
        /// A loaded <see cref="FileExtendedProperties" /> instance or a default one if not found.
        /// </returns>
        public ValueTask<FileExtendedProperties> GetExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file)
        {
            string extendedPropertiesPath = this.GetExtendedPropertiesPath(storeAbsolutePath, file);
            if (!File.Exists(extendedPropertiesPath))
            {
                return new ValueTask<FileExtendedProperties>(new FileExtendedProperties());
            }

            string content = File.ReadAllText(extendedPropertiesPath);
            return new ValueTask<FileExtendedProperties>(JsonConvert.DeserializeObject<FileExtendedProperties>(content));
        }

        /// <summary>
        /// Saves the extended properties for a file reference.
        /// </summary>
        /// <param name="storeAbsolutePath">The store absolute path.</param>
        /// <param name="file">The reference holding the file path.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task SaveExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file, FileExtendedProperties extendedProperties)
        {
            string extendedPropertiesPath = this.GetExtendedPropertiesPath(storeAbsolutePath, file);
            string toStore = JsonConvert.SerializeObject(extendedProperties);
            File.WriteAllText(extendedPropertiesPath, toStore);
            return Task.FromResult(0);
        }

        private string GetExtendedPropertiesPath(string storeAbsolutePath, IPrivateFileReference file)
        {
            string fullPath = Path.GetFullPath(storeAbsolutePath).TrimEnd(Path.DirectorySeparatorChar);
            string rootPath = Path.GetDirectoryName(fullPath);
            string storeName = Path.GetFileName(fullPath);

            string extendedPropertiesPath = Path.Combine(rootPath, string.Format(this.options.FolderNameFormat, storeName), file.Path + ".json");
            this.EnsurePathExists(extendedPropertiesPath);
            return extendedPropertiesPath;
        }

        private void EnsurePathExists(string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
