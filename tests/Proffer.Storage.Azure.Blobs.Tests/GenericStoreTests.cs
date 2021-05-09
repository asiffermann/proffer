namespace Proffer.Storage.Azure.Blobs.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.Azure.Blobs.Tests.Stubs;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature("IStore<TOptions>")]
    [Collection(nameof(AzureBlobsTestCollection))]
    public class GenericStoreTests
    {
        private readonly AzureBlobsFixture fixture;

        public GenericStoreTests(AzureBlobsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ListFiles_With_GenericStore()
        {
            IStore<AzureBlobsStoreOptionsStub> store = this.fixture.Services.GetRequiredService<IStore<AzureBlobsStoreOptionsStub>>();

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
