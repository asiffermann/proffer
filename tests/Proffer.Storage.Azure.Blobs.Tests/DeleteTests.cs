namespace Proffer.Storage.Azure.Blobs.Tests
{
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature(nameof(IFileReference.DeleteAsync))]
    [Collection(nameof(AzureBlobsTestCollection))]
    public class DeleteTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly AzureBlobsFixture fixture;

        public DeleteTests(AzureBlobsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_DeleteFile_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("Delete/ToDelete.txt");

            await file.DeleteAsync();

            Assert.Null(await store.GetAsync("Delete/ToDelete.txt"));
            Assert.NotNull(await store.GetAsync("Delete/ToSurvive.txt"));
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_DeleteFile_With_Store(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("Delete/ToDelete2.txt");

            await store.DeleteAsync(file);

            Assert.Null(await store.GetAsync("Delete/ToDelete2.txt"));
            Assert.NotNull(await store.GetAsync("Delete/ToSurvive2.txt"));
        }
    }
}
