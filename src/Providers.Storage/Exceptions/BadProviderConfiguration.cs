namespace Providers.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a provider was not properly configured.
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadProviderConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadProviderConfiguration"/> class.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        public BadProviderConfiguration(string providerName) 
            : base($"The provider '{providerName}' was not properly configured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadProviderConfiguration"/> class.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <param name="details">The details.</param>
        public BadProviderConfiguration(string providerName, string details)
            : base($"The providerName '{providerName}' was not properly configured. {details}")
        {
        }
    }
}
