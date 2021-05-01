namespace Proffer.Email
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Proffer.Email services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.TryAddTransient<IEmailSender, Internal.EmailSender>();
            return services;
        }
    }
}
