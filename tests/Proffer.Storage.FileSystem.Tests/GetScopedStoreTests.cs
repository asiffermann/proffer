namespace Proffer.Storage.FileSystem.Tests
{
    using System.Text;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature(nameof(IStorageFactory.GetScopedStore))]
    public class GetScopedStoreTests : Abstract.StoreTestsBase
    {
        [Theory, MemberData(nameof(ConfiguredScopedStoreNames))]
        public async Task Should_UpdateFile_With_ScopedStore(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetScopedStore(storeName);

            await store.InitAsync();

            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }
    }
}
