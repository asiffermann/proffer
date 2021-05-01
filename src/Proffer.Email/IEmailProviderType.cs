namespace Proffer.Email
{
    /// <summary>
    /// Builds providers using a particular messaging protocol or API.
    /// </summary>
    public interface IEmailProviderType
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Builds the provider.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        /// <returns>A new <see cref="IEmailProvider"/>.</returns>
        IEmailProvider BuildProvider(IEmailProviderOptions providerOptions);
    }
}