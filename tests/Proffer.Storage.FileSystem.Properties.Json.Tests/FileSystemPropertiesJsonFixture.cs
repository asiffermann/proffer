namespace Proffer.Storage.FileSystem.Properties.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.FileSystem.Configuration;
    using Proffer.Testing;
    using Storage;

    public class FileSystemPropertiesJsonFixture : ServiceProviderFixtureBase
    {
        public FileSystemPropertiesJsonFixture()
        {
            this.ParsedOptions = this.Services.GetService<IOptions<FileSystemParsedOptions>>().Value;

            this.ResetStores();
        }

        public string FixtureBasePath => Path.Combine(this.BasePath, "test-runs", this.Id);

        public string RootPath => Path.Combine(this.FixtureBasePath, "FileVault");

        public string SecondaryRootPath => Path.Combine(this.FixtureBasePath, "FileVault2");

        public FileSystemParsedOptions ParsedOptions { get; }

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
                .AddFileSystemStorage(this.RootPath)
                .AddFileSystemExtendedProperties();
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

            if (Directory.Exists(this.RootPath))
            {
                Directory.Delete(this.RootPath, true);
            }

            if (Directory.Exists(this.SecondaryRootPath))
            {
                Directory.Delete(this.SecondaryRootPath, true);
            }

            foreach (KeyValuePair<string, FileSystemStoreOptions> parsedStoreKvp in this.ParsedOptions.ParsedStores)
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
            if (!Directory.Exists(this.RootPath))
            {
                Directory.CreateDirectory(this.RootPath);
            }

            foreach (KeyValuePair<string, FileSystemStoreOptions> parsedStoreKvp in this.ParsedOptions.ParsedStores)
            {
                if (!Path.IsPathRooted(parsedStoreKvp.Value.RootPath))
                {
                    parsedStoreKvp.Value.RootPath = Path.Combine(this.RootPath, parsedStoreKvp.Value.RootPath);
                }

                this.ResetFileSystemStore(parsedStoreKvp.Value.AbsolutePath);
            }
        }

        private void ResetFileSystemStore(string absolutePath)
        {
            string contentDirectoryPath = Path.Combine(this.BasePath, "Stores", "DefaultContent");

            this.CopyContentDirectoryTo(contentDirectoryPath, absolutePath);
        }
    }
}
