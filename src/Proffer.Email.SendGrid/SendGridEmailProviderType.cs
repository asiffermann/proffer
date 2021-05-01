namespace Proffer.Email.SendGrid
{
    /// <summary>
    /// Builds <see cref="SendGridEmailProvider"/>.
    /// </summary>
    /// <seealso cref="IEmailProviderType" />
    public class SendGridEmailProviderType : IEmailProviderType
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => "SendGrid";

        /// <summary>
        /// Builds the provider.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        /// <returns>
        /// A new <see cref="IEmailProvider" />.
        /// </returns>
        public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
            => new SendGridEmailProvider(providerOptions);
    }
}
