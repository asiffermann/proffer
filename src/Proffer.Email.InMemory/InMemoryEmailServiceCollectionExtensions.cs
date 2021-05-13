namespace Proffer.Email
{
    using InMemory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class InMemoryEmailServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Email services to an in-memory collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddInMemoryEmail(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEmailProviderType, InMemoryEmailProviderType>());
            services.AddSingleton<IInMemoryEmailRepository, InMemoryEmailRepository>();
            return services;
        }
    }
}
