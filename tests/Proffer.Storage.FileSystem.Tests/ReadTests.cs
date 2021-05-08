namespace Proffer.Storage.FileSystem.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Collection(nameof(FileSystemTestCollection))]
    public class ReadTests : Abstract.ConfiguredStoresTestsBase
    {
        private readonly FileSystemFixture fixture;

        public ReadTests(FileSystemFixture fixture)
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
        public async Task Should_Throw_When_GettingFile_With_AbsoluteUri(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await store.GetAsync(new Uri("http://localhost/SubDirectory/TextFile2.txt"), false));
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
        public async Task Should_Throw_When_AccessingPublicUrl_Without_Server(string storeName)
        {
            IStore store = this.fixture.GetStore(storeName);

            IFileReference file = await store.GetAsync("SubDirectory/TextFile2.txt");

            Assert.Throws<InvalidOperationException>(() => file.PublicUrl);
        }
    }
}
