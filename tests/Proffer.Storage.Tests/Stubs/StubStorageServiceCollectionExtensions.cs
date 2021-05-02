namespace Proffer.Storage
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Storage.Internal;
    using Proffer.Storage.Tests.Stubs;
    using Proffer.Storage.Tests.Stubs.Configuration;

    public static class StubStorageServiceCollectionExtensions
    {
        public static IServiceCollection AddStubStorage(this IServiceCollection services)
        {
            services
                .AddSingleton<IConfigureOptions<StubParsedOptions>, ConfigureProviderOptions<StubParsedOptions, StubProviderInstanceOptions, StubStoreOptions, StubScopedStoreOptions>>()
                .TryAddEnumerable(ServiceDescriptor.Transient<IStorageProvider, StubStorageProvider>());

            return services;
        }
    }
}
