namespace Proffer.Templating
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Templating services to use <see cref="HandlebarsDotNet"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHandlebars(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<ITemplateProvider, Handlebars.HandlebarsTemplateProvider>());
            return services;
        }
    }
}
