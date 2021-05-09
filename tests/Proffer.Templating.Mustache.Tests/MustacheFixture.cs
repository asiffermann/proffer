namespace Proffer.Templating.Mustache.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Templating;
    using Proffer.Templating.Mustache.Tests.Stubs;
    using Proffer.Testing;

    public class MustacheFixture : ServiceProviderFixtureBase
    {
        public MustacheFixture()
        {
            this.Templates = this.Services.GetRequiredService<TemplateCollection>();
        }

        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        public TemplateCollection Templates { get; }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddTemplating()
                .AddMustache()
                .AddTransient<TemplateCollection>()
                .AddTransient<MustacheTemplateProvider>();
        }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            inMemoryCollectionData.Add("Storage:Stores:Templates:ProviderType", "FileSystem");
            inMemoryCollectionData.Add("Storage:Stores:OtherTemplates:ProviderType", "FileSystem");
            inMemoryCollectionData.Add("Storage:Stores:Partials:ProviderType", "FileSystem");
        }

        public class TemplateCollection : TemplateCollectionBase
        {
            public TemplateCollection(IStorageFactory storageFactory, ITemplateLoaderFactory templateLoaderFactory)
                : base("Templates", storageFactory, templateLoaderFactory)
            {
            }

            public Task<string> TitleBody(object context, IFormatProvider formatProvider = null)
                => this.LoadAndApplyTemplate("TitleBody", context, formatProvider);

            public Task<string> Contacts(ContactsContext context)
                => this.LoadAndApplyTemplate("Contacts", context);

            public Task<string> BadContext(object context)
                => this.LoadAndApplyTemplate("BadContext", context);

            internal Task<ITemplate> GetTemplate(string name) => this.Loader.GetTemplate(name);
        }
    }
}
