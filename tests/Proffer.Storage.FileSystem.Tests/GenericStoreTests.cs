namespace Proffer.Storage.FileSystem.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.FileSystem.Tests.Stubs;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature("IStore<TOptions>")]
    [Collection(nameof(FileSystemCollection))]
    public class GenericStoreTests
    {
        private readonly FileSystemFixture fixture;

        public GenericStoreTests(FileSystemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ListFiles_With_GenericStore()
        {
            IStore<FileSystemStoreOptionsStub> store = this.fixture.Services.GetRequiredService<IStore<FileSystemStoreOptionsStub>>();

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
