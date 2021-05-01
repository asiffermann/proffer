namespace Proffer.Storage.Azure.Test
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class DeleteTests
    {
        private readonly AzureFixture storeFixture;

        public DeleteTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory(DisplayName = nameof(Delete)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task Delete(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            IFileReference file = await store.GetAsync("Delete/ToDelete.txt");

            await file.DeleteAsync();

            Assert.Null(await store.GetAsync("Delete/ToDelete.txt"));
            Assert.NotNull(await store.GetAsync("Delete/ToSurvive.txt"));
        }
    }
}
