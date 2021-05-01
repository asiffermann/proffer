namespace Proffer.Storage.Configuration
{
    /// <summary>
    /// Options for an instance of <see cref="IStorageProvider"/>.
    /// </summary>
    /// <seealso cref="INamedElementOptions" />
    public interface IProviderInstanceOptions : INamedElementOptions
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        string Type { get; }
    }
}
