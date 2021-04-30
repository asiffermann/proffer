namespace Providers.Templating
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMustache(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<ITemplateProvider, Mustache.MustacheSharpTemplateProvider>());
            return services;
        }
    }
}
