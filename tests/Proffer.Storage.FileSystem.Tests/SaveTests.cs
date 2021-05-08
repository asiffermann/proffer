namespace Proffer.Storage.FileSystem.Tests
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Proffer.Storage.Exceptions;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Collection(nameof(FileSystemTestCollection))]
    public class SaveTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly FileSystemFixture fixture;

        public SaveTests(FileSystemFixture fixture)
        {
            this.fixture = fixture;
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
