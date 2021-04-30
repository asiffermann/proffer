namespace Providers.Templating
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the <see cref="global::Mustache"/> templating provider.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMustacheProvider(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<ITemplateProvider, Mustache.MustacheSharpTemplateProvider>());
            return services;
        }
    }
}
