namespace Proffer.Storage.Azure.Blobs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using global::Azure.Storage;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Sas;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Azure.Blobs.Configuration;
    using Proffer.Storage.Azure.Blobs.Tests.Stubs;
    using Proffer.Storage.Configuration;
    using Proffer.Testing;
    using Storage;

    public class AzureBlobsFixture : ServiceProviderFixtureBase
    {
        public AzureBlobsFixture()
        {
            this.ParsedOptions = this.Services.GetService<IOptions<AzureBlobsParsedOptions>>().Value;
            this.GenericStoreOptions = this.Services.GetService<IOptions<AzureBlobsStoreOptionsStub>>().Value
                .ParseStoreOptions<AzureBlobsParsedOptions, AzureBlobsProviderOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions>(this.ParsedOptions);

            this.InitStores();
        }

        public AzureBlobsParsedOptions ParsedOptions { get; }

        public AzureBlobsStoreOptions GenericStoreOptions { get; }

        private string AzCopy => Environment.ExpandEnvironmentVariables(this.Configuration["AzCopy10Command"]);

        private string ConnectionString => this.ParsedOptions.ConnectionStrings.Values.First();

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
            inMemoryCollectionData.Add("Storage:Stores:CustomConnectionStringProvider:FolderName", $"ccsp-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:CustomConnectionString:FolderName", $"ccs-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:ReferenceConnectionStringProvider:FolderName", $"rcsp-{this.Id}");
            inMemoryCollectionData.Add("Storage:Stores:ReferenceConnectionString:FolderName", $"rcs-{this.Id}");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddAzureStorage();

            services.Configure<AzureBlobsStoreOptionsStub>(o => o.ConnectionString = this.ConnectionString);
        }

        protected override void OnDispose()
        {
            foreach (KeyValuePair<string, AzureBlobsStoreOptions> parsedStoreKvp in this.ParsedOptions.ParsedStores)
            {
                var containerClient = new BlobContainerClient(this.ConnectionString, parsedStoreKvp.Value.FolderName);
                containerClient.Delete();
            }
        }

        private void InitStores()
        {
            this.InitStore(this.GenericStoreOptions);
            foreach (KeyValuePair<string, AzureBlobsStoreOptions> parsedStoreKvp in this.ParsedOptions.ParsedStores)
            {
                this.InitStore(parsedStoreKvp.Value);
            }
        }

        private void InitStore(AzureBlobsStoreOptions storeOptions)
        {
            var containerClient = new BlobContainerClient(storeOptions.ConnectionString, storeOptions.FolderName);
            containerClient.CreateIfNotExists();

            string defaultContentPath = Path.Combine(this.BasePath, "Stores", "DefaultContent", "*");
            string sasToken = GetAccountSasToken(storeOptions.ConnectionString);

            string arguments = $"copy \"{defaultContentPath}\" \"{containerClient.Uri}?{sasToken}\" --recursive";

            var processStartInfo = new ProcessStartInfo(this.AzCopy)
            {
                Arguments = arguments,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(processStartInfo))
            {
                if (!process.WaitForExit(30000))
                {
                    process.Kill();
                    throw new TimeoutException($"Azure Store '{storeOptions.Name}' was not reset properly.");
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
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
