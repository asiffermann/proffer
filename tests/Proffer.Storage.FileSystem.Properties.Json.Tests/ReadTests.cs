namespace Proffer.Storage.FileSystem.Properties.Json.Tests
{
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature(nameof(IExtendedPropertiesProvider))]
    [Collection(nameof(FileSystemPropertiesJsonCollection))]
    public class ReadTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly FileSystemPropertiesJsonFixture fixture;

        public ReadTests(FileSystemPropertiesJsonFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.FetchProperties))]
        public async Task Should_FetchProperties_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            await file.FetchProperties();

            var fileInfo = new System.IO.FileInfo("Stores/DefaultContent/SubDirectory/TextFile2.txt");

            Assert.Equal(fileInfo.Length, file.Properties.Length);
        }
    }
}
