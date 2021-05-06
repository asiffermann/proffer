namespace Proffer.Configuration
{
    /// <summary>
    /// Options of a provider.
    /// </summary>
    /// <seealso cref="INamedElementOptions" />
    public interface IProviderOptions : INamedElementOptions
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        string Type { get; }
    }
}
