namespace Proffer.Storage.Azure.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Proffer.Storage.Azure.Configuration;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature(nameof(IStore.GetSharedAccessSignatureAsync))]
    [Collection(nameof(AzureCollection))]
    public class GetSharedAccessSignatureTests
    {
        private readonly AzureFixture storeFixture;

        public GetSharedAccessSignatureTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory, InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListFiles_With_SharedAccessSignature(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();
            IOptions<AzureParsedOptions> options = this.storeFixture.Services.GetRequiredService<IOptions<AzureParsedOptions>>();

            IStore store = storageFactory.GetStore(storeName);

            options.Value.ParsedStores.TryGetValue(storeName, out AzureStoreOptions storeOptions);

            string sharedAccessSignature = await store.GetSharedAccessSignatureAsync(new SharedAccessPolicy
            {
                ExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessPermissions.List,
            });

            var account = CloudStorageAccount.Parse(storeOptions.ConnectionString);            

            var accountSAS = new StorageCredentials(sharedAccessSignature);
            var accountWithSAS = new CloudStorageAccount(accountSAS, account.Credentials.AccountName, endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClientWithSAS = accountWithSAS.CreateCloudBlobClient();
            CloudBlobContainer containerWithSAS = blobClientWithSAS.GetContainerReference(storeOptions.FolderName);

            BlobContinuationToken continuationToken = null;
            var results = new List<IListBlobItem>();

            do
            {
                BlobResultSegment response = await containerWithSAS.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            }
            while (continuationToken != null);

            IFileReference[] filesFromStore = await store.ListAsync(null, false, false);

            Assert.Equal(filesFromStore.Length, results.OfType<ICloudBlob>().Count());
        }
    }
}
