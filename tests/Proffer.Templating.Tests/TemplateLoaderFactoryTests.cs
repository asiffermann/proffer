namespace Proffer.Templating.Tests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(ITemplateLoaderFactory))]
    [Collection(nameof(TemplatingTestCollection))]
    public class TemplateLoaderFactoryTests
    {
        private readonly TemplatingFixture fixture;

        public TemplateLoaderFactoryTests(TemplatingFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Should_CreateTemplateLoader()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetRequiredService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetRequiredService<ITemplateLoaderFactory>();

            ITemplateLoader templateLoader = templateLoaderFactory.Create(storageFactory.GetStore("Templates"));

            Assert.NotNull(templateLoader);
        }

        [Fact]
        public void Should_CreateTemplateLoader_With_CustomScope()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetRequiredService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetRequiredService<ITemplateLoaderFactory>();

            ITemplateLoader templateLoader = templateLoaderFactory.Create(storageFactory.GetStore("OtherTemplates"), "OtherTemplates");

            Assert.NotNull(templateLoader);
        }

        [Fact]
        public void Should_Throw_When_CreatingTemplateLoader_With_NullStore()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetRequiredService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetRequiredService<ITemplateLoaderFactory>();

            Assert.Throws<ArgumentNullException>(() => templateLoaderFactory.Create(null));
        }
    }
}
