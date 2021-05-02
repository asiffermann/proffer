namespace Proffer.Storage.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Configuration;
    using Proffer.Storage.Tests.Stubs;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(StorageServiceCollectionExtensions))]
    public class StorageServiceCollectionExtensionsTests
    {
        [Fact]
        public void Should_RegisterGenericStore_When_CallingAddStorage()
        {
            var fixture = new SimpleServiceProviderFixture((sp, f) => sp.AddStorage().AddStubStorage());

            IStore<GenericStoreOptionsStub> genericStore = fixture.Services.GetService<IStore<GenericStoreOptionsStub>>();

            Assert.NotNull(genericStore);
        }

        [Fact]
        public void Should_RegisterStorageFactory_When_CallingAddStorage()
        {
            var fixture = new SimpleServiceProviderFixture((sp, f) => sp.AddStorage().AddStubStorage());

            IStorageFactory storageFactory = fixture.Services.GetService<IStorageFactory>();

            Assert.NotNull(storageFactory);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationSection()
        {
            string expectedStoreName = "DynamicOptionsStore";
            string expectedProviderName = "OnlyStub";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration.GetSection("Storage"))
                        .AddStubStorage();
                },
                new()
                {
                    { $"Storage:Stores:{expectedStoreName}:ProviderName", expectedProviderName }
                });

            IOptions<StorageOptions> storageOptions = fixture.Services.GetService<IOptions<StorageOptions>>();

            Assert.NotNull(storageOptions);
            Assert.NotNull(storageOptions.Value);

            bool storeOptionsFound = storageOptions.Value.ParsedStores
                .TryGetValue(expectedStoreName, out StoreOptions dynamicOptions);

            Assert.True(storeOptionsFound);

            Assert.Equal(expectedProviderName, dynamicOptions.ProviderName);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationRoot()
        {
            string expectedStoreName = "DynamicOptionsStore";
            string expectedProviderName = "OnlyStub";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddStubStorage();
                },
                new()
                {
                    { $"Storage:Stores:{expectedStoreName}:ProviderName", expectedProviderName }
                });

            IOptions<StorageOptions> storageOptions = fixture.Services.GetService<IOptions<StorageOptions>>();

            Assert.NotNull(storageOptions);
            Assert.NotNull(storageOptions.Value);

            bool storeOptionsFound = storageOptions.Value.ParsedStores
                .TryGetValue(expectedStoreName, out StoreOptions dynamicOptions);

            Assert.True(storeOptionsFound);

            Assert.Equal(expectedProviderName, dynamicOptions.ProviderName);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationRoot_With_CustomSectionName()
        {
            string sectionName = "CustomStorageSection";
            string expectedStoreName = "DynamicOptionsStore";
            string expectedProviderType = "Stub";

            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration, sectionName)
                        .AddStubStorage();
                },
                new()
                {
                    { $"{sectionName}:Stores:{expectedStoreName}:ProviderType", expectedProviderType }
                });

            IOptions<StorageOptions> storageOptions = fixture.Services.GetService<IOptions<StorageOptions>>();

            Assert.NotNull(storageOptions);
            Assert.NotNull(storageOptions.Value);

            bool storeOptionsFound = storageOptions.Value.ParsedStores
                .TryGetValue(expectedStoreName, out StoreOptions dynamicOptions);

            Assert.True(storeOptionsFound);

            Assert.Equal(expectedProviderType, dynamicOptions.ProviderType);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationRoot_With_ConnectionStrings()
        {
            var connectionStrings = new Dictionary<string, string>();
            var inMemoryConfiguration = new Dictionary<string, string>();

            int random = new Random().Next(2, 10);
            for (int i = 0; i < random; i++)
            {
                string connectionStringName = $"Database{i}";
                string connectionStringValue = $"<Secure token {i}>";

                connectionStrings.Add(connectionStringName, connectionStringValue);

                string connectionStringsPrefix = "ConnectionStrings";
                string configurationPrefix = ( i % 2 ) == 0 ? connectionStringsPrefix : $"Storage:{connectionStringsPrefix}";

                inMemoryConfiguration.Add($"{configurationPrefix}:{connectionStringName}", connectionStringValue);
            }

            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddStubStorage();
                },
                inMemoryConfiguration);

            IOptions<StorageOptions> storageOptions = fixture.Services.GetService<IOptions<StorageOptions>>();

            Assert.NotNull(storageOptions);
            Assert.NotNull(storageOptions.Value);

            Assert.All(
                storageOptions.Value.ConnectionStrings,
                kvp =>
                {
                    if (connectionStrings.ContainsKey(kvp.Key))
                    {
                        Assert.Equal(connectionStrings[kvp.Key], kvp.Value);
                    }
                });
        }
    }
}
