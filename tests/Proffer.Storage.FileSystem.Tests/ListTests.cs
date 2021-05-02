namespace Proffer.Storage.FileSystem.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature(nameof(IStore.ListAsync))]
    public class ListTests : Abstract.StoreTestsBase
    {
        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListRootFiles_When_PathIsNull(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListRootFiles_When_PathIsEmpty(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync("");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListDirectoryFiles_When_PathIsDirectory(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "SubDirectory/TextFile2.txt" };

            IFileReference[] results = await store.ListAsync("SubDirectory");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListDirectoryFiles_When_PathIsDirectoryWithTrailingSlash(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "SubDirectory/TextFile2.txt" };

            IFileReference[] results = await store.ListAsync("SubDirectory/");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsExtensionGlobbing(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "Globbing/template.hbs", "Globbing/template-header.hbs" };

            IFileReference[] results = await store.ListAsync("Globbing", "*.hbs");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsFileNameGlobbing(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "Globbing/template.hbs", "Globbing/template.mustache" };

            IFileReference[] results = await store.ListAsync("Globbing", "template.*");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsFileNameGlobbing_And_PathIsEmpty(string storeName, FileSystemFixture fixture)
        {
            IStore store = fixture.GetStore(storeName);

            string[] expected = new string[] { "template.hbs" };

            IFileReference[] results = await store.ListAsync("", "template.*");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
