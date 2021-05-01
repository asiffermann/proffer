namespace Proffer.Testing
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;

    public abstract class ServiceProviderFixture
    {
        public ServiceProviderFixture()
        {
            this.BasePath = PlatformServices.Default.Application.ApplicationBasePath;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(this.BasePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddMemoryCache();
            services.AddOptions();

            this.ConfigureServices(services);

            this.Services = services.BuildServiceProvider();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider Services { get; }

        public string BasePath { get; }

        protected abstract void ConfigureServices(IServiceCollection services);
    }
}
