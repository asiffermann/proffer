using System;

namespace Proffer.Email.Smtp
{
    /// <summary>
    /// Builds <see cref="SmtpEmailProvider"/>.
    /// </summary>
    /// <seealso cref="IEmailProviderType" />
    public class SmtpEmailProviderType : IEmailProviderType
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailProviderType"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public SmtpEmailProviderType(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => "Smtp";

        /// <summary>
        /// Builds the provider.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        /// <returns>
        /// A new <see cref="IEmailProvider" />.
        /// </returns>
        public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
            => new SmtpEmailProvider(this.serviceProvider, providerOptions);
    }
}
