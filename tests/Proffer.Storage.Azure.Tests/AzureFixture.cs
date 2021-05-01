namespace Proffer.Storage.Azure.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using global::Azure.Storage;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Sas;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Configuration;
    using Proffer.Storage.Azure.Tests.Stubs;
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
                var containerClient = new BlobContainerClient(parsedStoreKvp.Value.ConnectionString, parsedStoreKvp.Value.FolderName);
                containerClient.DeleteIfExists();
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
                Arguments = $"\"{Path.Combine(this.BasePath, "Stores", "DefaultContent")}\" \"{absolutePath}\" /MIR"
            });

            if (!process.WaitForExit(30000))
            {
                process.Kill();
                throw new TimeoutException($"FileSystem Store '{storeName}' was not reset properly.");
            }
        }

        private void ResetAzureStores()
        {
            string azcopy = Environment.ExpandEnvironmentVariables(this.Configuration["AzCopy10Command"]);

            foreach (KeyValuePair<string, AzureStoreOptions> parsedStoreKvp in this.AzureParsedOptions.ParsedStores)
            {
                var containerClient = new BlobContainerClient(parsedStoreKvp.Value.ConnectionString, parsedStoreKvp.Value.FolderName);
                containerClient.CreateIfNotExists();

                string defaultContentPath = Path.Combine(this.BasePath, "Stores", "DefaultContent", "*");
                string sasToken = GetAccountSasToken(parsedStoreKvp.Value.ConnectionString);

                string arguments = $"copy \"{defaultContentPath}\" \"{containerClient.Uri}?{sasToken}\" --recursive";

                var processStartInfo = new ProcessStartInfo(azcopy)
                {
                    Arguments = arguments,
                    RedirectStandardOutput = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    if (!process.WaitForExit(30000))
                    {
                        process.Kill();
                        throw new TimeoutException($"Azure Store '{parsedStoreKvp.Key}' was not reset properly.");
                    }

                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }
            }
        }

        private static string GetAccountSasToken(string connectionString)
        {
            var sasBuilder = new AccountSasBuilder()
            {
                Services = AccountSasServices.Blobs | AccountSasServices.Files,
                ResourceTypes = AccountSasResourceTypes.All,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
                Protocol = SasProtocol.Https
            };

            sasBuilder.SetPermissions(AccountSasPermissions.Read | AccountSasPermissions.Write);

            (string accountName, string accountKey) = GetKeyValueFromConnectionString(connectionString);

            return sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(accountName, accountKey)).ToString();
        }

        private static (string accountName, string accountKey) GetKeyValueFromConnectionString(string connectionString)
        {
            IDictionary<string, string> settings = new Dictionary<string, string>();
            string[] splitted = connectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string nameValue in splitted)
            {
                string[] splittedNameValue = nameValue.Split(new char[] { '=' }, 2);
                settings.Add(splittedNameValue[0], splittedNameValue[1]);
            }

            return (settings["AccountName"], settings["AccountKey"]);
        }
    }
}
