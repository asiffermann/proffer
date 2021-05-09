namespace Proffer.Email
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Smtp;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Proffer.Email services to a SMTP server with <see cref="MailKit"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddSmtpEmail(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEmailProviderType, SmtpEmailProviderType>());
            services.AddTransient<ISmtpClient, SmtpClient>();
            return services;
        }
    }
}
