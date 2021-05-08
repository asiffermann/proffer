namespace Proffer.Templating.Tests
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(TemplateCollectionBase<StoreBase>))]
    public class TypedStoreTemplateCollectionBaseTests
    {
        private class TypedStore : StoreBase
        {
            public TypedStore(IStorageFactory storageFactory)
                : base("OtherTemplates", storageFactory)
            {
            }

            public async Task CreateTemplateAsync()
            {
                string templateContent = "Hello typed store template! Today is {0}.";
                await this.Store.SaveAsync(Encoding.UTF8.GetBytes(templateContent), "NewTemplate.stub", "text/x-stub-template");
            }
        }

        private class TypedStoreTemplateCollection : TemplateCollectionBase<TypedStore>
        {
            public TypedStoreTemplateCollection(TypedStore store, ITemplateLoaderFactory templateLoaderFactory)
                : base(store, templateLoaderFactory)
            {
            }

            public Task<string> ApplyNewTemplate(DateTime context)
            {
                return this.LoadAndApplyTemplate("NewTemplate", context);
            }

            public Task<string> ApplyNewTemplate(DateTime context, IFormatProvider formatProvider)
            {
                return this.LoadAndApplyTemplate("NewTemplate", context, formatProvider);
            }
        }

        [Fact]
        public async Task Should_LoadAndApply_NewTemplate()
        {
            var fixture = new SimpleServiceProviderFixture(
                (services, fixture) =>
                {
                    services
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddStubTemplating()
                        .AddTransient<TypedStore>()
                        .AddTransient<TypedStoreTemplateCollection>();
                });

            TypedStore store = fixture.Services.GetService<TypedStore>();
            await store.CreateTemplateAsync();

            TypedStoreTemplateCollection templateCollection = fixture.Services.GetService<TypedStoreTemplateCollection>();

            DateTime now = DateTime.Now.Date;
            string result = await templateCollection.ApplyNewTemplate(now);

            Assert.NotNull(result);
            Assert.Equal($"Hello typed store template! Today is {now}.", result);
        }

        [Fact]
        public async Task Should_LoadAndApply_NewTemplate_With_Format()
        {
            var fixture = new SimpleServiceProviderFixture(
                (services, fixture) =>
                {
                    services
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddStubTemplating()
                        .AddTransient<TypedStore>()
                        .AddTransient<TypedStoreTemplateCollection>();
                });

            TypedStore store = fixture.Services.GetService<TypedStore>();
            await store.CreateTemplateAsync();

            TypedStoreTemplateCollection templateCollection = fixture.Services.GetService<TypedStoreTemplateCollection>();

            DateTime now = DateTime.Now.Date;
            string result = await templateCollection.ApplyNewTemplate(now, CultureInfo.GetCultureInfo("fr-FR"));

            Assert.NotNull(result);
            Assert.Equal($"Hello typed store template! Today is {now:dd/MM/yyyy HH:mm:ss}.", result);
        }
    }
}
