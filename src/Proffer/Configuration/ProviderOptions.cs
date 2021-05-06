namespace Proffer.Configuration
{
    /// <summary>
    /// Standard options for a provider.
    /// </summary>
    /// <seealso cref="IProviderOptions" />
    public class ProviderOptions : IProviderOptions
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
