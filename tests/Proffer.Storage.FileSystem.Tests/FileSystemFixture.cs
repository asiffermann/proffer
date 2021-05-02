namespace Proffer.Storage.FileSystem.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Configuration;
    using Proffer.Storage.FileSystem.Configuration;
    using Proffer.Storage.FileSystem.Tests.Stubs;
    using Proffer.Testing;
    using Storage;

    public class FileSystemFixture : ServiceProviderFixtureBase
    {
        public FileSystemFixture()
        {
            this.StorageOptions = this.Services.GetService<IOptions<StorageOptions>>().Value;
            this.FileSystemParsedOptions = this.Services.GetService<IOptions<FileSystemParsedOptions>>().Value;
            this.TestStoreOptions = this.Services.GetService<IOptions<FileSystemStoreOptionsStub>>().Value
                .ParseStoreOptions<FileSystemParsedOptions, FileSystemProviderInstanceOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions>(this.FileSystemParsedOptions);

            this.ResetStores();
        }

        public string FixtureBasePath => Path.Combine(this.BasePath, "test-runs", this.Id);

        public string FileSystemRootPath => Path.Combine(this.FixtureBasePath, "FileVault");

        public string FileSystemSecondaryRootPath => Path.Combine(this.FixtureBasePath, "FileVault2");

        public StorageOptions StorageOptions { get; }

        public FileSystemParsedOptions FileSystemParsedOptions { get; }

        public FileSystemStoreOptions TestStoreOptions { get; }

        public IStore GetStore(string storeName)
        {
            IStorageFactory storageFactory = this.Services.GetRequiredService<IStorageFactory>();

            return storageFactory.GetStore(storeName);
        }

        public IStore GetScopedStore(string storeName)
        {
            IStorageFactory storageFactory = this.Services.GetRequiredService<IStorageFactory>();

            return storageFactory.GetScopedStore(storeName, Guid.NewGuid());
        }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.FileSystemRootPath);

            services.Configure<FileSystemStoreOptionsStub>(o => { });
        }

        protected override void OnDispose()
        {
            this.DeleteRootResources();
        }

        private void DeleteRootResources()
        {
            if (Directory.Exists(this.FixtureBasePath))
            {
                Directory.Delete(this.FixtureBasePath, true);
            }

            if (Directory.Exists(this.FileSystemRootPath))
            {
                Directory.Delete(this.FileSystemRootPath, true);
            }

            if (Directory.Exists(this.FileSystemSecondaryRootPath))
            {
                Directory.Delete(this.FileSystemSecondaryRootPath, true);
            }

            foreach (KeyValuePair<string, FileSystemStoreOptions> parsedStoreKvp in this.FileSystemParsedOptions.ParsedStores)
            {
                if (Directory.Exists(parsedStoreKvp.Value.AbsolutePath))
                {
                    Directory.Delete(parsedStoreKvp.Value.AbsolutePath, true);
                }
            }
        }

        private void ResetStores()
        {
            this.DeleteRootResources();

            Directory.CreateDirectory(this.FixtureBasePath);

            this.ResetFileSystemStores();
        }

        private void ResetFileSystemStores()
        {
            if (!Directory.Exists(this.FileSystemRootPath))
            {
                Directory.CreateDirectory(this.FileSystemRootPath);
            }

            foreach (KeyValuePair<string, FileSystemStoreOptions> parsedStoreKvp in this.FileSystemParsedOptions.ParsedStores)
            {
                if (!Path.IsPathRooted(parsedStoreKvp.Value.RootPath))
                {
                    parsedStoreKvp.Value.RootPath = Path.Combine(this.FileSystemRootPath, parsedStoreKvp.Value.RootPath);
                }

                this.ResetFileSystemStore(parsedStoreKvp.Value.AbsolutePath);
            }

            this.ResetFileSystemStore(this.TestStoreOptions.AbsolutePath);
        }

        private void ResetFileSystemStore(string absolutePath)
        {
            string contentDirectoryPath = Path.Combine(this.BasePath, "Stores", "DefaultContent");

            this.CopyContentDirectoryTo(contentDirectoryPath, absolutePath);
        }

        private void CopyContentDirectoryTo(string contentDirectoryPath, string destinationPath)
        {
            var contentDirectory = new DirectoryInfo(contentDirectoryPath);

            if (!contentDirectory.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + contentDirectoryPath);
            }

            FileInfo[] files = contentDirectory.GetFiles();
            DirectoryInfo[] directories = contentDirectory.GetDirectories();

            Directory.CreateDirectory(destinationPath);
            foreach (FileInfo file in files)
            {
                string destinationFilePath = Path.Combine(destinationPath, file.Name);
                file.CopyTo(destinationFilePath, false);
            }

            foreach (DirectoryInfo subDirectory in directories)
            {
                string subDirectoryDestinationPath = Path.Combine(destinationPath, subDirectory.Name);
                this.CopyContentDirectoryTo(subDirectory.FullName, subDirectoryDestinationPath);
            }
        }
    }
}
