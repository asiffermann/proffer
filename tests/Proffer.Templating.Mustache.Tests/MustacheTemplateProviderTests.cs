namespace Proffer.Templating.Mustache.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(Mustache))]
    [Feature(nameof(MustacheTemplateProvider))]
    [Collection(nameof(MustacheTestCollection))]
    public class MustacheTemplateProviderTests
    {
        private readonly MustacheFixture fixture;

        public MustacheTemplateProviderTests(MustacheFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_GetTemplate()
        {
            ITemplate template = await this.fixture.Templates.GetTemplate("TitleBody");

            Assert.NotNull(template);

            string result = template.Apply(new { Title = "Sample title", Body = "Sample body" });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Should_CompileTemplate_With_StringContent()
        {
            MustacheTemplateProvider provider = this.fixture.Services.GetRequiredService<MustacheTemplateProvider>();

            ITemplate template = provider.Compile("Hello {{Name}}!");

            Assert.NotNull(template);

            string result = template.Apply(new { Name = "World" });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public async Task Should_Throws_When_StoreContainsPartials()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetRequiredService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetRequiredService<ITemplateLoaderFactory>();

            IStore store = storageFactory.GetStore("Partials");
            ITemplateLoader loader = templateLoaderFactory.Create(store);

            await Assert.ThrowsAsync<NotSupportedException>(() => loader.GetTemplate("Hello"));
        }
    }
}
