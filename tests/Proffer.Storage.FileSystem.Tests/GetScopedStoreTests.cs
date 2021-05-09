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
    [Collection(nameof(FileSystemTestCollection))]
    public class GetScopedStoreTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly FileSystemFixture fixture;

        public GetScopedStoreTests(FileSystemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ConfiguredScopedStoreNames))]
        public async Task Should_UpdateFile_With_ScopedStore(string storeName)
        {
            IStore store = this.fixture.GetScopedStore(storeName);

            await store.InitAsync();

            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }
    }
}
