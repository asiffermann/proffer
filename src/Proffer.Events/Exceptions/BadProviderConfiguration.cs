namespace Proffer.Events.Exceptions
{
    using System;

    /// <summary>
    /// An exception thrown when the provider configuration is malformed
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadProviderConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadProviderConfiguration"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public BadProviderConfiguration(string providerName)
            : base($"The Provider \"{providerName}\" was not properly configured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadProviderConfiguration"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="details">The details.</param>
        public BadProviderConfiguration(string providerName, string details)
            : base($"The Provider \"{providerName}\" was not properly configured. {details}")
        {
        }
    }
}
