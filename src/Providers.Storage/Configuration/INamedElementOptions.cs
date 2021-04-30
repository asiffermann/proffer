namespace Providers.Storage.Configuration
{
    /// <summary>
    /// A named element from the options (provider or scope).
    /// </summary>
    public interface INamedElementOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }
    }
}
