namespace Proffer.Storage.Azure.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.Azure.Tests.Stubs;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Feature("IStore<TOptions>")]
    [Collection(nameof(AzureCollection))]
    public class GenericStoreTests
    {
        private readonly AzureFixture fixture;

        public GenericStoreTests(AzureFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ListFiles_With_GenericStore()
        {
            IStore<AzureStoreOptionsStub> store = this.fixture.Services.GetRequiredService<IStore<AzureStoreOptionsStub>>();

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
