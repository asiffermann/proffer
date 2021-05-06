namespace Proffer.Storage.Azure.Blobs.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Proffer.Storage.Exceptions;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureBlobsCollection))]
    public class SaveTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly AzureBlobsFixture fixture;

        public SaveTests(AzureBlobsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_PreserveETag_When_SavingSameContent(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);
            string textToWrite = "ETag Test Compute";
            string filePath = "Update/etag-same.txt";

            IFileReference savedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");
            IFileReference readReference = await store.GetAsync(filePath, withMetadata: true);

            Assert.Equal(savedReference.Properties.ETag, readReference.Properties.ETag);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_ChangeETag_When_SavingDifferentContent(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string textToWrite = "ETag Test Compute";
            string filePath = "Update/etag-different.txt";
            string textToUpdate = "ETag Test Compute 2";

            IFileReference savedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");
            IFileReference updatedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToUpdate), filePath, "text/plain");

            Assert.NotEqual(savedReference.Properties.ETag, updatedReference.Properties.ETag);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.SavePropertiesAsync))]
        public async Task Should_SaveProperties_When_AddingMetadata(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata.Add("newid", id);

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualId = file.Properties.Metadata["newid"];

            Assert.Equal(id, actualId);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.SavePropertiesAsync))]
        public async Task Should_SaveProperties_When_SettingMetadata(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata["id"] = id;

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualId = file.Properties.Metadata["id"];

            Assert.Equal(id, actualId);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.SavePropertiesAsync))]
        public async Task Should_SaveProperties_With_PreservedEncoding(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string name = "ï";

            file.Properties.Metadata["name"] = name;

            await file.SavePropertiesAsync();

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualName = file.Properties.Metadata["name"];

            Assert.Equal(name, actualName);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.SavePropertiesAsync))]
        public async Task Should_GetMetadata_When_ListingFiles_With_UpdatedMetadata(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

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

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.SavePropertiesAsync))]
        public async Task Should_SaveProperties_When_SavingContent(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string testFile = "Metadata/TextFile.txt";

            IFileReference file = await store.GetAsync(testFile, withMetadata: true);

            string id = Guid.NewGuid().ToString();

            file.Properties.Metadata["SavingContentId"] = id;

            string textToWrite = "Hello";
            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), file, "text/plain", metadata: file.Properties.Metadata);

            file = await store.GetAsync(testFile, withMetadata: true);

            string actualId = file.Properties.Metadata["SavingContentId"];

            Assert.Equal(id, actualId);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_SaveFileContent_With_ByteArray(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_SaveFileContent_With_Stream(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42-2.txt";

            await store.SaveAsync(new MemoryStream(Encoding.UTF8.GetBytes(textToWrite)), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_Throw_With_ExistingFile_When_PassingNever(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);
            string filePath = "TextFile.txt";

            await Assert.ThrowsAsync<FileAlreadyExistsException>(
                async () => await store.SaveAsync(
                    new MemoryStream(Encoding.UTF8.GetBytes("The answer is 42")),
                    filePath,
                    "text/plain",
                    OverwritePolicy.Never));
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.UpdateAsync))]
        public async Task Should_UpdateFileContent_With_FileRefence(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/TextFile.txt";

            IFileReference file = await store.GetAsync(filePath);

            await file.UpdateAsync(new MemoryStream(Encoding.UTF8.GetBytes(textToWrite)));

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }
    }
}
