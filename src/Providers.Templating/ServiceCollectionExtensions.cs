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
        /// Registers the templating providers basic services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddTemplatingProviders(this IServiceCollection services)
        {
            services.TryAddTransient<ITemplateLoaderFactory, Internal.TemplateLoaderFactory>();
            return services;
        }
    }
}
