namespace Proffer.Testing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;

    public abstract class ServiceProviderFixture : IDisposable
    {
        private bool disposedValue;

        public ServiceProviderFixture()
        {
            this.Id = Guid.NewGuid().ToString("N").ToLower();
            this.BasePath = PlatformServices.Default.Application.ApplicationBasePath;

            var inMemoryCollectionData = new Dictionary<string, string>();
            this.AddInMemoryCollectionConfiguration(inMemoryCollectionData);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(this.BasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddInMemoryCollection(inMemoryCollectionData)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddMemoryCache();
            services.AddOptions();

            this.ConfigureServices(services);

            this.Services = services.BuildServiceProvider();
        }

        public string Id { get; }

        public string BasePath { get; }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected abstract void ConfigureServices(IServiceCollection services);

        protected virtual void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData) { }

        protected virtual void OnDispose() { }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.OnDispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
