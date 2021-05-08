namespace Proffer.Templating.Tests
{
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
    }
}
