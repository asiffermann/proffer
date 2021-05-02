namespace Proffer.Storage.Azure.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [IntegrationTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Azure))]
    [Collection(nameof(AzureCollection))]
    public class ReadTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly AzureFixture fixture;

        public ReadTests(AzureFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_When_PathIsRootFile(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = "42";

            string actualText = await store.ReadAllTextAsync("TextFile.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_When_PathIsNestedFile(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            string actualText = await store.ReadAllTextAsync("SubDirectory/TextFile2.txt");

            Assert.Equal(expectedText, actualText);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAllBytesAsync))]
        public async Task Should_ReadAllBytes_When_PathIsNestedFile(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            using (var reader = new StreamReader(new MemoryStream(await store.ReadAllBytesAsync("SubDirectory/TextFile2.txt"))))
            {
                string actualText = reader.ReadToEnd();
                Assert.Equal(expectedText, actualText);
            }
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.ReadAllBytesAsync))]
        public async Task Should_ReadAllBytes_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            using (var reader = new StreamReader(new MemoryStream(await file.ReadAllBytesAsync())))
            {
                string actualText = reader.ReadToEnd();
                Assert.Equal(expectedText, actualText);
            }
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAllBytesAsync))]
        public async Task Should_ReadAllBytes_With_Store(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            using (var reader = new StreamReader(new MemoryStream(await store.ReadAllBytesAsync(file))))
            {
                string actualText = reader.ReadToEnd();
                Assert.Equal(expectedText, actualText);
            }
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.ReadAsync))]
        public async Task Should_Read_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = null;

            using (var reader = new StreamReader(await file.ReadAsync()))
            {
                actualText = await reader.ReadToEndAsync();
            }

            Assert.Equal(expectedText, actualText);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAsync))]
        public async Task Should_Read_With_Store(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = null;

            using (var reader = new StreamReader(await store.ReadAsync(file)))
            {
                actualText = await reader.ReadToEndAsync();
            }

            Assert.Equal(expectedText, actualText);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.ReadAllTextAsync))]
        public async Task Should_ReadAllText_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string actualText = await file.ReadAllTextAsync();

            Assert.Equal(expectedText, actualText);
        }


        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.ReadAllTextAsync))]
        public async Task Should_ReadAllText_With_EachListingFileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            string expectedText = ">42";

            IFileReference[] files = await store.ListAsync("SubDirectory");

            foreach (IFileReference file in files)
            {
                string actualText = await store.ReadAllTextAsync(file);

                Assert.Equal(expectedText, actualText);
            }
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.GetAsync))]
        public async Task Should_GetFile_With_Uri(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync(new Uri("SubDirectory/TextFile2.txt", UriKind.Relative), false);

            Assert.NotNull(file);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.GetAsync))]
        public async Task Should_GetFile_With_AbsoluteUri(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference fileByPath = await store.GetAsync("SubDirectory/TextFile2.txt", true);
            IFileReference file = await store.GetAsync(new Uri(fileByPath.PublicUrl), true);

            Assert.NotNull(file);
            Assert.Equal(fileByPath.Properties.ETag, file.Properties.ETag);
            Assert.Equal(fileByPath.Properties.CacheControl, file.Properties.CacheControl);
            Assert.Equal(fileByPath.Properties.ContentType, file.Properties.ContentType);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.GetAsync))]
        public async Task Should_Throw_When_AccessingProperties_Without_LoadingIt(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            Assert.Throws<InvalidOperationException>(() => file.Properties.ContentType);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IStore.GetAsync))]
        public async Task Should_AccessPublicUrl_Without_Server(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            string publicUrl = file.PublicUrl;

            Assert.NotNull(publicUrl);
        }

        [Theory, MemberData(nameof(ConfiguredStoreNames))]
        [Feature(nameof(IFileReference.FetchProperties))]
        public async Task Should_FetchProperties_With_FileReference(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            await file.FetchProperties();

            var fileInfo = new FileInfo("Stores/DefaultContent/SubDirectory/TextFile2.txt");

            Assert.Equal(fileInfo.Length, file.Properties.Length);
        }
    }
}
