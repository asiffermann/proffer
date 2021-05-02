namespace Proffer.Storage.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(StoreBase))]
    public class StoreBaseTests
    {
        public class PublicStore : StoreBase
        {
            public PublicStore(IStorageFactory storageFactory)
                : base(nameof(PublicStore), storageFactory)
            {
            }
        }

        [Fact]
        public void Should_GetTypedStore_PublicStore()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage().AddTransient<PublicStore>());

            PublicStore publicStore = fixture.Services.GetService<PublicStore>();

            Assert.NotNull(publicStore);
            Assert.NotNull(publicStore.Store);
            Assert.Equal(nameof(PublicStore), publicStore.Store.Name);
        }
    }
}
