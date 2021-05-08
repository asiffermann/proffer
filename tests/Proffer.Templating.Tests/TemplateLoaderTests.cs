namespace Proffer.Templating.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(ITemplateLoader))]
    [Collection(nameof(TemplatingTestCollection))]
    public class TemplateLoaderTests
    {
        private readonly TemplatingFixture fixture;

        public TemplateLoaderTests(TemplatingFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_GetTemplate_With_SimplePath()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetService<ITemplateLoaderFactory>();
            ITemplateLoader templateLoader = templateLoaderFactory.Create(storageFactory.GetStore("Templates"));

            ITemplate template = await templateLoader.GetTemplate("SimpleTemplate");

            Assert.NotNull(template);
        }

        [Fact]
        public async Task Should_GetTemplate_With_FolderPath()
        {
            IStorageFactory storageFactory = this.fixture.Services.GetService<IStorageFactory>();
            ITemplateLoaderFactory templateLoaderFactory = this.fixture.Services.GetService<ITemplateLoaderFactory>();
            ITemplateLoader templateLoader = templateLoaderFactory.Create(storageFactory.GetStore("Templates"));

            ITemplate template = await templateLoader.GetTemplate("SubFolder/TemplateInFolder");

            Assert.NotNull(template);
        }
    }
}
