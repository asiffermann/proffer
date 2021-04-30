namespace Providers.Storage.FileSystem
{
    using Microsoft.Extensions.Options;
    using Providers.Storage.FileSystem.Configuration;
    using Providers.Storage.Internal;
    using Storage;

    /// <summary>
    /// A provider to handle and build file stores pointing on a File System directory.
    /// </summary>
    /// <seealso cref="StorageProviderBase{TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions}" />
    public class FileSystemStorageProvider : StorageProviderBase<FileSystemParsedOptions, FileSystemProviderInstanceOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions>
    {
        /// <summary>
        /// The <see cref="FileSystemStorageProvider"/> name.
        /// </summary>
        public const string ProviderName = "FileSystem";

        private readonly IPublicUrlProvider publicUrlProvider;
        private readonly IExtendedPropertiesProvider extendedPropertiesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorageProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="publicUrlProvider">The public URL provider.</param>
        /// <param name="extendedPropertiesProvider">The extended properties provider.</param>
        public FileSystemStorageProvider(IOptions<FileSystemParsedOptions> options, IPublicUrlProvider publicUrlProvider = null, IExtendedPropertiesProvider extendedPropertiesProvider = null)
            : base(options)
        {
            this.publicUrlProvider = publicUrlProvider;
            this.extendedPropertiesProvider = extendedPropertiesProvider;
        }

        /// <summary>
        /// Gets the name of this provider.
        /// </summary>
        public override string Name => ProviderName;

        /// <summary>
        /// Provider-specific build of a store with specific options.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns>
        /// A configured <see cref="IStore" />.
        /// </returns>
        protected override IStore BuildStoreInternal(string storeName, FileSystemStoreOptions storeOptions)
            => new FileSystemStore(
                storeOptions,
                this.publicUrlProvider,
                this.extendedPropertiesProvider);
    }
}
