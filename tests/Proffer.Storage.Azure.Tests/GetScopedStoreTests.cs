namespace Proffer.Storage.Azure.Tests
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
    [Feature(nameof(IStorageFactory.GetScopedStore))]
    [Collection(nameof(AzureCollection))]
    public class GetScopedStoreTests
    {
        private readonly AzureFixture storeFixture;

        public GetScopedStoreTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory, InlineData("ScopedStore1"), InlineData("ScopedStore2")]
        public async Task Should_UpdateFile_With_ScopedStore(string storeName)
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
