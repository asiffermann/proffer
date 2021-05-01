namespace Proffer.Email.Smtp
{
    /// <summary>
    /// Builds <see cref="SmtpEmailProvider"/>.
    /// </summary>
    /// <seealso cref="IEmailProviderType" />
    public class SmtpEmailProviderType : IEmailProviderType
    {
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
            => new SmtpEmailProvider(providerOptions);
    }
}
