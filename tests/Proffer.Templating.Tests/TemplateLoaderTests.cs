namespace Proffer.Templating.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Templating.Exceptions;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(ITemplateLoader))]
    [Collection(nameof(TemplatingTestCollection))]
    public class TemplateLoaderTests
    {
        private readonly TemplatingFixture fixture;
        private readonly IStorageFactory storageFactory;
        private readonly ITemplateLoaderFactory templateLoaderFactory;
        private readonly ITemplateLoader templateLoader;

        public TemplateLoaderTests(TemplatingFixture fixture)
        {
            this.fixture = fixture;

            this.storageFactory = this.fixture.Services.GetRequiredService<IStorageFactory>();
            this.templateLoaderFactory = this.fixture.Services.GetRequiredService<ITemplateLoaderFactory>();
            this.templateLoader = this.templateLoaderFactory.Create(this.storageFactory.GetStore("Templates"));
        }

        [Fact]
        public async Task Should_GetTemplate_With_SimplePath()
        {
            ITemplate template = await this.templateLoader.GetTemplate("SimpleTemplate");

            Assert.NotNull(template);
        }

        [Fact]
        public async Task Should_GetTemplate_With_FolderPath()
        {
            ITemplate template = await this.templateLoader.GetTemplate("SubFolder/TemplateInFolder");

            Assert.NotNull(template);
        }

        [Fact]
        public async Task Should_Throw_With_MissingTemplate()
        {
            await Assert.ThrowsAsync<TemplateNotFoundException>(() => this.templateLoader.GetTemplate("MissingTemplate"));
        }

        [Fact]
        public async Task Should_Throw_With_MissingProvider()
        {
            await Assert.ThrowsAsync<ProviderNotFoundException>(() => this.templateLoader.GetTemplate("MissingProvider"));
        }
    }
}
