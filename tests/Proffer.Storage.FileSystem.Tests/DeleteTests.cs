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
    public class DeleteTests : Abstract.StoreTestsBase
    {
        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_DeleteFile_With_FileReference(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("Delete/ToDelete.txt");

            await file.DeleteAsync();

            Assert.Null(await store.GetAsync("Delete/ToDelete.txt"));
            Assert.NotNull(await store.GetAsync("Delete/ToSurvive.txt"));
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_DeleteFile_With_Store(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("Delete/ToDelete.txt");

            await store.DeleteAsync(file);

            Assert.Null(await store.GetAsync("Delete/ToDelete.txt"));
            Assert.NotNull(await store.GetAsync("Delete/ToSurvive.txt"));
        }
    }
}
