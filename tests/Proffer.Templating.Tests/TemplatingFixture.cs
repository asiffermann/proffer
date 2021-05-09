namespace Proffer.Templating.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Storage;
    using Proffer.Testing;

    public class TemplatingFixture : ServiceProviderFixtureBase
    {
        public string StorageRootPath => Path.Combine(this.BasePath, "Stores");

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.StorageRootPath)
                .AddTemplating()
                .AddStubTemplating();
        }

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            inMemoryCollectionData.Add("Storage:Stores:Templates:ProviderType", "FileSystem");
            inMemoryCollectionData.Add("Storage:Stores:OtherTemplates:ProviderType", "FileSystem");
        }
    }
}
