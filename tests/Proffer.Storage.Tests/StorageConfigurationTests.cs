namespace Proffer.Storage.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Storage.Configuration;
    using Proffer.Storage.Exceptions;
    using Proffer.Storage.Tests.Stubs.Configuration;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(Configuration))]
    public class StorageConfigurationTests
    {
        [Fact]
        [Bug("https://github.com/asiffermann/proffer/issues/47")]
        public void Should_ValidateStoreOptions_Without_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IOptions<StorageOptions> options = fixture.Services.GetRequiredService<IOptions<StorageOptions>>();

            IEnumerable<IOptionError> errors = options.Value.ParsedStores
                .SelectMany(s => s.Value.Validate(throwOnError: false));

            Assert.Empty(errors);
        }

        [Fact]
        public void Should_ValidateStubStoreOptions_Without_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration).AddStubStorage());

            IOptions<StubParsedOptions> options = fixture.Services.GetRequiredService<IOptions<StubParsedOptions>>();

            IEnumerable<IOptionError> errors = options.Value.ParsedStores
                .SelectMany(s => s.Value.Validate(throwOnError: false));

            Assert.Empty(errors);
        }

        [Fact]
        public void Should_NotValidateStoreOptions_With_MissingName()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";
            string providerType = "Stub";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:Name", "" },
                    { $"{sectionName}:Stores:{storeName}:ProviderType", providerType }
                });

            IOptions<StubParsedOptions> options = fixture.Services.GetRequiredService<IOptions<StubParsedOptions>>();
            StubStoreOptions storeOptions = options.Value.ParsedStores[storeName];

            IEnumerable<IOptionError> errors = storeOptions.Validate(throwOnError: false);
            IOptionError missingNameError = errors.FirstOrDefault(e => e.PropertyName == nameof(StoreOptions.Name));

            Assert.NotNull(missingNameError);
            Assert.Equal("Name should be defined.", missingNameError.ErrorMessage);
        }

        [Fact]
        public void Should_NotValidateStoreOptions_With_MissingProvider()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:Name", storeName },
                });

            IOptions<StorageOptions> options = fixture.Services.GetRequiredService<IOptions<StorageOptions>>();
            StoreOptions storeOptions = options.Value.ParsedStores[storeName];

            IEnumerable<IOptionError> errors = storeOptions.Validate(throwOnError: false);
            IOptionError missingProviderError = errors.FirstOrDefault(e => e.PropertyName.Contains("Provider"));

            Assert.NotNull(missingProviderError);
        }

        [Fact]
        [Bug("https://github.com/asiffermann/proffer/issues/47")]
        public void Should_ValidateStoreOptions_With_MissingFolderName()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:Name", storeName },
                    { $"{sectionName}:Stores:{storeName}:ProviderType", "Stub" },
                });

            IOptions<StorageOptions> options = fixture.Services.GetRequiredService<IOptions<StorageOptions>>();
            StoreOptions storeOptions = options.Value.ParsedStores[storeName];

            IEnumerable<IOptionError> errors = storeOptions.Validate(throwOnError: false);

            Assert.Empty(errors);
        }

        [Fact]
        public void Should_ValidateStubStoreOptions_With_MissingFolderName()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:Name", storeName },
                    { $"{sectionName}:Stores:{storeName}:ProviderType", "Stub" },
                });

            IOptions<StubParsedOptions> options = fixture.Services.GetRequiredService<IOptions<StubParsedOptions>>();
            StubStoreOptions storeOptions = options.Value.ParsedStores[storeName];

            IEnumerable<IOptionError> errors = storeOptions.Validate(throwOnError: false);

            Assert.Empty(errors);
        }

        [Fact]
        public void Should_ThrowException_Without_ExplicitlyOptOut()
        {
            string sectionName = "BadStorage";
            string storeName = "ConfiguredStore";

            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddStorage(f.Configuration.GetSection(sectionName)).AddStubStorage(),
                new()
                {
                    { $"{sectionName}:Stores:{storeName}:Name", storeName },
                });

            IOptions<StorageOptions> options = fixture.Services.GetRequiredService<IOptions<StorageOptions>>();
            StoreOptions storeOptions = options.Value.ParsedStores[storeName];

            Assert.Throws<BadStoreConfiguration>(() => storeOptions.Validate());
        }
    }
}
