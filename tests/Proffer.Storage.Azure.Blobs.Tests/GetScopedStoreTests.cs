namespace Proffer.Storage.Azure.Blobs.Tests
{
    using System.Text;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature(nameof(IStorageFactory.GetScopedStore))]
    [Collection(nameof(AzureBlobsTestCollection))]
    public class GetScopedStoreTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly AzureBlobsFixture fixture;

        public GetScopedStoreTests(AzureBlobsFixture fixture)
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
