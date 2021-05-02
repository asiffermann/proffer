namespace Proffer.Storage.Azure.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature(nameof(IStore.ListAsync))]
    [Collection(nameof(AzureCollection))]
    public class ListTests
    {
        private readonly AzureFixture storeFixture;

        public ListTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListRootFiles_When_PathIsNull(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListRootFiles_When_PathIsEmpty(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync("");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListDirectoryFiles_When_PathIsDirectory(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "SubDirectory/TextFile2.txt" };

            IFileReference[] results = await store.ListAsync("SubDirectory");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListDirectoryFiles_When_PathIsDirectoryWithTrailingSlash(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "SubDirectory/TextFile2.txt" };

            IFileReference[] results = await store.ListAsync("SubDirectory/");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsExtensionGlobbing(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "Globbing/template.hbs", "Globbing/template-header.hbs" };

            IFileReference[] results = await store.ListAsync("Globbing", "*.hbs");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsFileNameGlobbing(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "Globbing/template.hbs", "Globbing/template.mustache" };

            IFileReference[] results = await store.ListAsync("Globbing", "template.*");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Should_ListMatchingFiles_When_SearchPatternIsFileNameGlobbing_And_PathIsEmpty(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string[] expected = new string[] { "template.hbs" };

            IFileReference[] results = await store.ListAsync("", "template.*");

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
