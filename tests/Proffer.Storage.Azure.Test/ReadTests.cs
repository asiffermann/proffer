namespace Proffer.Storage.Azure.Test
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

        [Theory(DisplayName = nameof(ReadAllTextFromRootFile)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadAllTextFromRootFile(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = "42";

            string actualText = await store.ReadAllTextAsync("TextFile.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory(DisplayName = nameof(ReadAllTextFromRootFile)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadAllTextFromSubdirectoryFile(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            string actualText = await store.ReadAllTextAsync("SubDirectory/TextFile2.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory(DisplayName = nameof(ReadAllBytesFromSubdirectoryFile)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadAllBytesFromSubdirectoryFile(string storeName)
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

        [Theory(DisplayName = nameof(ReadAllBytesFromSubdirectoryFileUsingFileReference)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadAllBytesFromSubdirectoryFileUsingFileReference(string storeName)
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


        [Theory(DisplayName = nameof(ReadFileFromSubdirectoryFile)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadFileFromSubdirectoryFile(string storeName)
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

        [Theory(DisplayName = nameof(ReadAllTextFromSubdirectoryFileUsingFileReference)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ReadAllTextFromSubdirectoryFileUsingFileReference(string storeName)
        {
            IStorageFactory storageFactory = this.storeFixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = await file.ReadAllTextAsync();

            Assert.Equal(expectedText, actualText);
        }


        [Theory(DisplayName = nameof(ListThenReadAllTextFromSubdirectoryFile)), InlineData("Store1"), InlineData("Store2"), InlineData("Store3"), InlineData("Store4"), InlineData("Store5"), InlineData("Store6")]
        public async Task ListThenReadAllTextFromSubdirectoryFile(string storeName)
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
