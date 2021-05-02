namespace Proffer.Storage.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage.Tests.Stubs;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(StubStorageServiceCollectionExtensions))]
    public class StorageServiceCollectionExtensionsTests
    {
        [Fact]
        public void Should_RegisterGenericStore_When_CallingAddStorage()
        {
            var fixture = new SimpleServiceProviderFixture(sp => sp.AddStorage().AddStubStorage());

            IStore<GenericStoreOptionsStub> genericStore = fixture.Services.GetService<IStore<GenericStoreOptionsStub>>();

            Assert.NotNull(genericStore);
        }

        [Fact]
        public void Should_RegisterStorageFactory_When_CallingAddStorage()
        {
            var fixture = new SimpleServiceProviderFixture(sp => sp.AddStorage().AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetService<IStorageFactory>();

            Assert.NotNull(storageFactory);
        }
    }
}
