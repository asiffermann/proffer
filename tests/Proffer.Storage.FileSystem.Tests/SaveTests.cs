namespace Proffer.Storage.FileSystem.Tests
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    public class SaveTests : Abstract.StoreTestsBase
    {
        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_SaveFileContent_With_ByteArray(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_ChangeETag_When_SavingDifferentContent(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);
            string textToWrite = "ETag Test Compute";
            string filePath = "Update/etag-different.txt";
            string textToUpdate = "ETag Test Compute 2";

            IFileReference savedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");
            IFileReference updatedReference = await store.SaveAsync(Encoding.UTF8.GetBytes(textToUpdate), filePath, "text/plain");

            Assert.NotEqual(savedReference.Properties.ETag, updatedReference.Properties.ETag);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.SaveAsync))]
        public async Task Should_SaveFileContent_With_Stream(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);
            string textToWrite = "The answer is 42";
            string filePath = "Update/42-2.txt";

            await store.SaveAsync(new MemoryStream(Encoding.UTF8.GetBytes(textToWrite)), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }
    }
}
