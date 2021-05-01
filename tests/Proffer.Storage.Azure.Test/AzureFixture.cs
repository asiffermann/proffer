namespace Proffer.Storage.Azure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Proffer.Storage.Azure.Configuration;
    using Proffer.Storage.Azure.Test.Stubs;
    using Proffer.Storage.Configuration;
    using Proffer.Storage.FileSystem.Configuration;
    using Proffer.Testing;
    using Storage;

    public class AzureFixture : ServiceProviderFixture
    {
        public AzureFixture()
        {
            this.StorageOptions = this.Services.GetService<IOptions<StorageOptions>>().Value;
            this.AzureParsedOptions = this.Services.GetService<IOptions<AzureParsedOptions>>().Value;
            this.FileSystemParsedOptions = this.Services.GetService<IOptions<FileSystemParsedOptions>>().Value;
            this.TestStoreOptions = this.Services.GetService<IOptions<StoreOptionsStub>>().Value.ParseStoreOptions<FileSystemParsedOptions, FileSystemProviderInstanceOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions>(this.FileSystemParsedOptions);

            this.ResetStores();
        }

        public string FileSystemRootPath => Path.Combine(this.BasePath, "FileVault");

        public string FileSystemSecondaryRootPath => Path.Combine(this.BasePath, "FileVault2");

        public StorageOptions StorageOptions { get; }

        public AzureParsedOptions AzureParsedOptions { get; }

        public FileSystemParsedOptions FileSystemParsedOptions { get; }

        public FileSystemStoreOptions TestStoreOptions { get; }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            inMemoryCollectionData.Add("Storage:Stores:Store3:FolderName", $"Store3-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:Store4:FolderName", $"Store4-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:Store5:FolderName", $"Store5-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:Store6:FolderName", $"Store6-{this.Id}");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddAzureStorage()
                .AddFileSystemStorage(this.FileSystemRootPath)
                .AddFileSystemExtendedProperties();

            services.Configure<StoreOptionsStub>(o => { });
        }

        protected override void OnDispose()
        {
            this.DeleteRootResources();
        }

        private void DeleteRootResources()
        {
            foreach (KeyValuePair<string, AzureStoreOptions> parsedStoreKvp in this.AzureParsedOptions.ParsedStores)
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(parsedStoreKvp.Value.ConnectionString);

                CloudBlobClient client = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference(parsedStoreKvp.Value.FolderName);

                container.DeleteIfExistsAsync().Wait();
            }

            if (Directory.Exists(this.FileSystemRootPath))
            {
                Directory.Delete(this.FileSystemRootPath, true);
            }

            if (Directory.Exists(this.FileSystemSecondaryRootPath))
            {
                Directory.Delete(this.FileSystemSecondaryRootPath, true);
            }
        }

        private void ResetStores()
        {
            this.DeleteRootResources();
            this.ResetAzureStores();
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
                this.ResetFileSystemStore(parsedStoreKvp.Key, parsedStoreKvp.Value.AbsolutePath);
            }

            this.ResetFileSystemStore(this.TestStoreOptions.Name, this.TestStoreOptions.AbsolutePath);
        }

        private void ResetFileSystemStore(string storeName, string absolutePath)
        {
            var process = Process.Start(new ProcessStartInfo("robocopy.exe")
            {
                Arguments = $"\"{Path.Combine(this.BasePath, "SampleDirectory")}\" \"{absolutePath}\" /MIR"
            });

            if (!process.WaitForExit(30000))
            {
                process.Kill();
                throw new TimeoutException($"FileSystem Store '{storeName}' was not reset properly.");
            }
        }

        private void ResetAzureStores()
        {
            string azCopy = Path.Combine(
                Environment.ExpandEnvironmentVariables(this.Configuration["AzCopyPath"]),
                "AzCopy.exe");

            foreach (KeyValuePair<string, AzureStoreOptions> parsedStoreKvp in this.AzureParsedOptions.ParsedStores)
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(parsedStoreKvp.Value.ConnectionString);
                string cloudStoragekey = cloudStorageAccount.Credentials.ExportBase64EncodedKey();
                string containerName = parsedStoreKvp.Value.FolderName;

                string dest = cloudStorageAccount.BlobStorageUri.PrimaryUri.ToString() + containerName;

                CloudBlobClient client = cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = client.GetContainerReference(containerName);
                container.CreateIfNotExistsAsync().Wait();

                string arguments = $"/Source:\"{Path.Combine(this.BasePath, "SampleDirectory")}\" /Dest:\"{dest}\" /DestKey:{cloudStoragekey} /S /y";
                var process = Process.Start(new ProcessStartInfo(azCopy)
                {
                    Arguments = arguments
                });

                if (!process.WaitForExit(30000))
                {
                    process.Kill();
                    throw new TimeoutException($"Azure Store '{parsedStoreKvp.Key}' was not reset properly.");
                }
            }
        }
    }
}
