namespace Proffer.Email
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class EmailServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Proffer.Email services and configures it from the given <paramref name="configurationRoot" /> at section <paramref name="sectionName" />.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <param name="sectionName">The name of the section.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfigurationRoot configurationRoot, string sectionName = EmailOptions.DefaultConfigurationSectionName)
        {
            return services
                .AddEmail(configurationRoot.GetSection(sectionName));
        }

        /// <summary>
        /// Registers Proffer.Email services and configures it with the given section.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configurationSection">The configuration section.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services
                .Configure<EmailOptions>(configurationSection)
                .TryAddTransient<IEmailSender, Internal.EmailSender>();

            return services;
        }
    }
}
