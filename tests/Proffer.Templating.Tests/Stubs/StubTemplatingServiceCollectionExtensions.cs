namespace Proffer.Templating
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Proffer.Templating.Tests.Stubs;

    public static class StubTemplatingServiceCollectionExtensions
    {
        public static IServiceCollection AddStubTemplating(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<ITemplateProvider, StubTemplateProvider>());

            return services;
        }
    }
}
