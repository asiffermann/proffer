namespace Proffer.Email
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using SendGrid;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Email services to the <see cref="global::SendGrid"/> API.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddSendGridEmail(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEmailProviderType, SendGridEmailProviderType>());
            return services;
        }
    }
}
