namespace Proffer.Templating.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(TemplateCollectionBase))]
    public class TemplateCollectionBaseTests
    {
        private class TemplateCollection : TemplateCollectionBase
        {
            public TemplateCollection(IStorageFactory storageFactory, ITemplateLoaderFactory templateLoaderFactory)
                : base("Templates", storageFactory, templateLoaderFactory)
            {
            }

            public Task<string> ApplySimpleTemplate(string context)
            {
                return this.LoadAndApplyTemplate("SimpleTemplate", context);
            }

            public Task<string> ApplyTemplateInFolder(string context)
            {
                return this.LoadAndApplyTemplate("SubFolder/TemplateInFolder", context);
            }
        }

        [Fact]
        public async Task Should_LoadAndApply_SimpleTemplate()
        {
            var fixture = new SimpleServiceProviderFixture(
                (services, fixture) =>
                {
                    services
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddStubTemplating()
                        .AddTransient<TemplateCollection>();
                });

            TemplateCollection templateCollection = fixture.Services.GetService<TemplateCollection>();

            string result = await templateCollection.ApplySimpleTemplate("Simple string context");

            Assert.NotNull(result);
            Assert.Equal("This is the context: Simple string context", result);
        }

        [Fact]
        public async Task Should_LoadAndApply_TemplateInFolder()
        {
            var fixture = new SimpleServiceProviderFixture(
                (services, fixture) =>
                {
                    services
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddStubTemplating()
                        .AddTransient<TemplateCollection>();
                });

            TemplateCollection templateCollection = fixture.Services.GetService<TemplateCollection>();

            string result = await templateCollection.ApplyTemplateInFolder("Simple string context");

            Assert.NotNull(result);
            Assert.Equal("Simple string context, you are in a folder.", result);
        }
    }
}
