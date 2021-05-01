namespace Proffer.Storage.Azure.Test
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class UpdateTests
    {
        private readonly AzureFixture storeFixture;

        public UpdateTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory(DisplayName = nameof(WriteAllText)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task WriteAllText(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }

        [Theory(DisplayName = nameof(ETagShouldBeTheSameWithSameContent)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ETagShouldBeTheSameWithSameContent(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);
            string textToWrite = "ETag Test Compute";
            string filePath = "Update/etag-same.txt";

            IFileReference savedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");
            IFileReference readReference = await store.GetAsync(filePath, withMetadata: true);

            Assert.Equal(savedReference.Properties.ETag, readReference.Properties.ETag);
        }

        [Theory(DisplayName = nameof(ETagShouldBeDifferentWithDifferentContent)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ETagShouldBeDifferentWithDifferentContent(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);
            string textToWrite = "ETag Test Compute";
            string filePath = "Update/etag-different.txt";
            string textToUpdate = "ETag Test Compute 2";

            IFileReference savedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");
            IFileReference updatedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToUpdate), filePath, "text/plain");

            Assert.NotEqual(savedReference.Properties.ETag, updatedReference.Properties.ETag);
        }

        [Theory(DisplayName = nameof(SaveStream)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task SaveStream(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42-2.txt";

            await store.SaveAsync(new MemoryStream(Encoding.UTF8.GetBytes(textToWrite)), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }

        [Theory(DisplayName = nameof(AddMetatadaRoundtrip)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task AddMetatadaRoundtrip(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata.Add("newid", id);

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualId = file.Properties.Metadata["newid"];

            Assert.Equal(id, actualId);
        }

        [Theory(DisplayName = nameof(SaveMetatadaRoundtrip)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task SaveMetatadaRoundtrip(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata["id"] = id;

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualId = file.Properties.Metadata["id"];

            Assert.Equal(id, actualId);
        }

        [Theory(DisplayName = nameof(SaveEncodedMetatadaRoundtrip)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task SaveEncodedMetatadaRoundtrip(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string name = "ï";

            file.Properties.Metadata["name"] = name;

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualName = file.Properties.Metadata["name"];

            Assert.Equal(name, actualName);
        }

        [Theory(DisplayName = nameof(ListMetatadaRoundtrip)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ListMetatadaRoundtrip(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata["id"] = id;

            await file.SavePropertiesAsync();

            IFileReference[] files = await store.ListAsync("Metadata", withMetadata: true);

            string actualId = null;

            foreach (IFileReference aFile in files)
            {
                if (aFile.Path == testFile)
                {
                    actualId = aFile.Properties.Metadata["id"];
                }
            }

            Assert.Equal(id, actualId);
        }
    }
}
