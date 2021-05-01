namespace Proffer.Storage.Azure.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.Azure.Test.Stubs;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class GenericIStoreTests
    {
        private readonly AzureFixture storeFixture;

        public GenericIStoreTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Fact]
        public async Task GenericListRootFiles()
        {
            IStore<StoreOptionsStub> store = this.storeFixture.Services.GetRequiredService<IStore<StoreOptionsStub>>();

            string[] expected = new string[] { "TextFile.txt", "template.hbs" };

            IFileReference[] results = await store.ListAsync(null);

            string[] missingFiles = expected.Except(results.Select(f => f.Path)).ToArray();

            string[] unexpectedFiles = results.Select(f => f.Path).Except(expected).ToArray();

            Assert.Empty(missingFiles);
            Assert.Empty(unexpectedFiles);
        }
    }
}
