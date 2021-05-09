namespace Proffer.Templating
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class TemplatingServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Proffer.Templating services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddTemplating(this IServiceCollection services)
        {
            services.TryAddTransient<ITemplateLoaderFactory, Internal.TemplateLoaderFactory>();
            return services;
        }
    }
}
