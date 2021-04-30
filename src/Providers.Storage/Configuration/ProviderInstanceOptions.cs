namespace Providers.Storage.Configuration
{
    /// <summary>
    /// Generic options for an instance of <see cref="IStorageProvider"/>.
    /// </summary>
    /// <seealso cref="IProviderInstanceOptions" />
    public class ProviderInstanceOptions : IProviderInstanceOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public string Type { get; set; }
    }
}
