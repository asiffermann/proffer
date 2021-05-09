namespace Proffer.Storage.Tests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.Exceptions;
    using Proffer.Storage.Tests.Stubs;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(IStorageProvider))]
    public class StorageFactoryTests
    {
        [Fact]
        public void Should_GetStore_DefaultStore()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore("DefaultStore");

            Assert.NotNull(store);
        }

        [Fact]
        public void Should_GetStore_DefaultProviderStore()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            IStore store = storageFactory.GetStore("DefaultProviderStore");

            Assert.NotNull(store);
        }

        [Fact]
        public void Should_TryGetStore_DefaultProviderStore()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore("DefaultProviderStore", out IStore store);

            Assert.True(storeFound);
            Assert.NotNull(store);
        }

        [Fact]
        public void Should_TryGetStore_DefaultProviderStore_With_ProviderCheck()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore("DefaultProviderStore", out IStore store, "Stub");

            Assert.True(storeFound);
            Assert.NotNull(store);
        }

        [Fact]
        public void Should_GetStore_With_Options()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            string storeName = Guid.NewGuid().ToString();
            IStore store = storageFactory.GetStore(storeName, new GenericStoreOptionsStub { Name = storeName });

            Assert.NotNull(store);
        }

        [Fact]
        public void Should_GetScopedStore_StubScopedStore()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            var scopedStoreId = Guid.NewGuid();
            IStore store = storageFactory.GetScopedStore("StubScopedStore", scopedStoreId);

            Assert.NotNull(store);
            Assert.Equal($"Stub-{scopedStoreId}", ( store as StubStore ).FolderName);
        }

        [Fact]
        public void Should_Throw_When_GettingUnknownScopedStore()
        {
            string storeName = "UnknownScopedStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<StoreNotFoundException>(() => storageFactory.GetScopedStore(storeName, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Throw_When_GettingScopedStore_With_BadFormat()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerType = "Stub";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:ScopedStores:{storeName}:ProviderType", providerType },
                    { $"{sectionName}:ScopedStores:{storeName}:FolderNameFormat", "Missing argument {0} {1}" }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<BadScopedStoreConfiguration>(() => storageFactory.GetScopedStore(storeName, Guid.NewGuid()));
        }

        [Fact]
        public void Should_ReturnNull_When_TryingToGetStore_With_InvalidProviderCheck()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore("DefaultProviderStore", out IStore store, "BadProvider");

            Assert.False(storeFound);
            Assert.Null(store);
        }

        [Fact]
        public void Should_ReturnNull_When_TryingToGetUnknownStore()
        {
            string storeName = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore(storeName, out IStore store);

            Assert.False(storeFound);
            Assert.Null(store);
        }

        [Fact]
        public void Should_Throw_When_GettingUnknownStore()
        {
            string storeName = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<StoreNotFoundException>(() => storageFactory.GetStore(storeName));
        }

        [Fact]
        public void Should_ReturnNull_When_TryingToGetStore_With_UnknownProviderName()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerName = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:ProviderName", providerName }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore(storeName, out IStore store);

            Assert.False(storeFound);
            Assert.Null(store);
        }

        [Fact]
        public void Should_Throw_When_GettingStore_With_UnknownProviderName()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerName = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:ProviderName", providerName }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<BadProviderConfiguration>(() => storageFactory.GetStore(storeName));
        }

        [Fact]
        public void Should_ReturnNull_When_TryingToGetStore_With_UnknownProviderType()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerType = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:ProviderType", providerType }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore(storeName, out IStore store);

            Assert.False(storeFound);
            Assert.Null(store);
        }

        [Fact]
        public void Should_Throw_When_GettingStore_With_UnknownProviderType()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerType = Guid.NewGuid().ToString();

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:ProviderType", providerType }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<ProviderNotFoundException>(() => storageFactory.GetStore(storeName));
        }

        [Fact]
        public void Should_ReturnNull_When_TryingToGetStore_Without_ProviderInfo()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:AccessLevel", "Private" }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            bool storeFound = storageFactory.TryGetStore(storeName, out IStore store);

            Assert.False(storeFound);
            Assert.Null(store);
        }

        [Fact]
        public void Should_Throw_When_GettingStore_Without_ProviderInfo()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:AccessLevel", "Private" }
                });

            IStorageFactory storageFactory = fixture.Services.GetRequiredService<IStorageFactory>();

            Assert.Throws<BadStoreConfiguration>(() => storageFactory.GetStore(storeName));
        }
    }
}
