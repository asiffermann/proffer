namespace Proffer.Events.Tests.Stubs
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Proffer.Events.Internal;
    using Proffer.Events.Tests.Stubs.Configuration;

    public static class StubServiceCollectionExtensions
    {
        public static IServiceCollection AddStubQueue(this IServiceCollection services)
        {
            services
                .AddScoped<IConfigureOptions<StubOptions>, ConfigureProviderOptions<StubOptions, StubProviderOptions, StubQueueOptions>>()
                .TryAddEnumerable(ServiceDescriptor.Transient<IEventProvider, StubEventProvider>());

            return services;
        }

        //public static IServiceCollection AddAzureStorageServices(this IServiceCollection services)
        //{
        //    services.TryAddEnumerable(ServiceDescriptor.Transient<IEventProvider, StubEventProvider>());
        //    return services;
        //}


    }
}
