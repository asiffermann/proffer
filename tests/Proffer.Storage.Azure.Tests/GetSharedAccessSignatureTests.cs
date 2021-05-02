namespace Proffer.Storage.Azure.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
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
            var containerClientReference = new BlobContainerClient(storeOptions.ConnectionString, storeOptions.FolderName);

            string sharedAccessSignature = await store.GetSharedAccessSignatureAsync(new SharedAccessPolicy
            {
                ExpiryTime = DateTime.UtcNow.AddHours(1),
                Permissions = SharedAccessPermissions.List,
            });

            var credentials = new AzureSasCredential(sharedAccessSignature);
            var containerClient = new BlobContainerClient(containerClientReference.Uri, credentials);

            var results = new List<BlobItem>();
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                results.Add(blobItem);
            }

            IFileReference[] filesFromStore = await store.ListAsync(null, recursive: true, false);

            Assert.Equal(filesFromStore.Length, results.Count);
        }
    }
}
