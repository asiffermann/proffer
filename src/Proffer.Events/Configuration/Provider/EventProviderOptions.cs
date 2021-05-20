namespace Proffer.Events.Configuration.Provider
{
    /// <summary>
    /// Generic options for Event Provider
    /// </summary>
    /// <seealso cref="IEventProviderOptions" />
    public class AzureStorageProviderOptions : IEventProviderOptions
    {
        /// <summary>
        /// Gets the provider type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the provider name.
        /// </summary>
        public string Name { get; set; }
    }
}
