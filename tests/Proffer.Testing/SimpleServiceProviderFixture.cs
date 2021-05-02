namespace Proffer.Testing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public class SimpleServiceProviderFixture : ServiceProviderFixtureBase
    {
        private readonly Action<IServiceCollection, SimpleServiceProviderFixture> configureServices;
        private readonly IDictionary<string, string> inMemoryConfiguration;

        public SimpleServiceProviderFixture(Action<IServiceCollection, SimpleServiceProviderFixture> configureServices, Dictionary<string, string> inMemoryConfiguration = null)
            : base(false)
        {
            this.configureServices = configureServices;
            this.inMemoryConfiguration = inMemoryConfiguration;
            this.Build();
        }

        protected override void ConfigureServices(IServiceCollection services)
            => this.configureServices(services, this);

        protected override void AddInMemoryCollectionConfiguration(IDictionary<string, string> inMemoryCollectionData)
        {
            if (this.inMemoryConfiguration == null)
            {
                return;
            }

            foreach (KeyValuePair<string, string> kvp in this.inMemoryConfiguration)
            {
                inMemoryCollectionData.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
