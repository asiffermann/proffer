namespace Proffer.Storage.FileSystem.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;
    using Proffer.Storage.FileSystem.Configuration;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(StorageServiceCollectionExtensions))]
    public class FileSystemStorageServiceCollectionExtensionsTests
    {
        [Fact]
        public void Should_RegisterProvider_When_CallingAddFileSystemStorage()
        {
            var fixture = new SimpleServiceProviderFixture((sp, f) => sp.AddStorage().AddFileSystemStorage());

            IEnumerable<IStorageProvider> providers = fixture.Services.GetService<IEnumerable<IStorageProvider>>();

            Assert.Single(providers);
            Assert.Equal("FileSystem", providers.First().Name);
        }

        [Fact]
        public void Should_DeduceRootPath_When_NotPassingItExplicitly()
        {
            var fixture = new SimpleServiceProviderFixture((sp, f) => sp.AddStorage().AddFileSystemStorage());

            IOptions<FileSystemParsedOptions> options = fixture.Services.GetService<IOptions<FileSystemParsedOptions>>();

            Assert.Equal(PlatformServices.Default.Application.ApplicationBasePath.TrimEnd('\\').TrimEnd('/'), options.Value.RootPath);
        }

        [Fact]
        public void Should_SetRootPath_When_PassingItExplicitly()
        {
            string rootPath = "C:/Custom";
            var fixture = new SimpleServiceProviderFixture((sp, f) => sp.AddStorage().AddFileSystemStorage(rootPath));

            IOptions<FileSystemParsedOptions> options = fixture.Services.GetService<IOptions<FileSystemParsedOptions>>();

            Assert.Equal(rootPath, options.Value.RootPath);
        }

        [Fact]
        public async Task Should_Throw_When_SavingProperties_With_DefaultExtendedPropertiesProvider()
        {
            string storeName = "Basic";
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddFileSystemStorage(),
                new()
                {
                    { $"Storage:Stores:{storeName}:ProviderType", "FileSystem" }
                });

            IOptions<FileSystemParsedOptions> options = fixture.Services.GetService<IOptions<FileSystemParsedOptions>>();

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();
            IStore store = storageFactory.GetStore(storeName);

            string fileName = "Throws.txt";
            await store.SaveAsync(
                System.IO.File.ReadAllBytes("Stores/DefaultContent/TextFile.txt"),
                fileName,
                "text/plain");

            IFileReference file = await store.GetAsync(fileName, withMetadata: true);
            file.Properties.Metadata.Add("newid", Guid.NewGuid().ToString());

            await Assert.ThrowsAsync<NotSupportedException>(async () => await file.SavePropertiesAsync());
        }
    }
}
