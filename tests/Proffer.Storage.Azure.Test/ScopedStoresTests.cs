namespace Proffer.Storage.Azure.Test
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class ScopedStoresTests
    {
        private readonly AzureFixture storeFixture;

        public ScopedStoresTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory(DisplayName = nameof(ScopedStoreUpdate)), InlineData("ScopedStore1"), InlineData("ScopedStore2")]
        public async Task ScopedStoreUpdate(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            var formatArg = Guid.NewGuid();
            IStore store = storageFactory.GetScopedStore(storeName, formatArg);

            await store.InitAsync();

            string textToWrite = "The answer is 42";
            string filePath = "Update/42.txt";

            await store.SaveAsync(Encoding.UTF8.GetBytes(textToWrite), filePath, "text/plain");

            string readFromWrittenFile = await store.ReadAllTextAsync(filePath);

            Assert.Equal(textToWrite, readFromWrittenFile);
        }
    }
}
