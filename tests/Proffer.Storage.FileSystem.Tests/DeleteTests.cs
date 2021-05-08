namespace Proffer.Storage.FileSystem.Tests
{
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature(nameof(IFileReference.DeleteAsync))]
    [Collection(nameof(FileSystemTestCollection))]
    public class DeleteTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly FileSystemFixture fixture;

        public DeleteTests(FileSystemFixture fixture)
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
