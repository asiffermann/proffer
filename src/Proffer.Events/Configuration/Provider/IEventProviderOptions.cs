namespace Proffer.Events.Configuration.Provider
{
    using Proffer.Configuration;

    /// <summary>
    /// An interface for Event Providers generic option
    /// </summary>
    /// <seealso cref="INamedElementOptions" />
    public interface IEventProviderOptions : INamedElementOptions
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        string Type { get; }
    }
}
