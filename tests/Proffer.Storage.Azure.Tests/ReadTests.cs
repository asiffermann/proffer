namespace Proffer.Storage.Azure.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class ReadTests
    {
        private readonly AzureFixture storeFixture;

        public ReadTests(AzureFixture fixture)
        {
            this.storeFixture = fixture;
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_When_PathIsRootFile(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = "42";

            string actualText = await store.ReadAllTextAsync("TextFile.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_When_PathIsNestedFile(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            string actualText = await store.ReadAllTextAsync("SubDirectory/TextFile2.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IStore.ReadAllBytesAsync))]
        public async Task Should_ReadAllBytes_When_PathIsNestedFile(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            using (var reader = new StreamReader(new MemoryStream(await store.ReadAllBytesAsync("SubDirectory/TextFile2.txt"))))
            {
                string actualText = reader.ReadToEnd();
                Assert.Equal(expectedText, actualText);
            }
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IFileReference.ReadAllBytesAsync))]
        public async Task Should_ReadAllBytes_With_FileReference(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            using (var reader = new StreamReader(new MemoryStream(await file.ReadAllBytesAsync())))
            {
                string actualText = reader.ReadToEnd();
                Assert.Equal(expectedText, actualText);
            }
        }


        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IFileReference.ReadAsync))]
        public async Task Should_Read_With_FileReference(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = null;

            using (var reader = new StreamReader(await file.ReadAsync()))
            {
                actualText = await reader.ReadToEndAsync();
            }

            Assert.Equal(expectedText, actualText);
        }

        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IFileReference.ReadAllTextAsync))]
        public async Task Should_ReadAllText_With_FileReference(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = await file.ReadAllTextAsync();

            Assert.Equal(expectedText, actualText);
        }


        [Theory, InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_With_EachListingFileReference(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            IFileReference[] files = await store.ListAsync("SubDirectory");

            foreach (IFileReference file in files)
            {
                string actualText = await store.ReadAllTextAsync(file);

                Assert.Equal(expectedText, actualText);
            }
        }
    }
}
