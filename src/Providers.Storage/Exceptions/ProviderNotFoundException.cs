namespace Providers.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a configured provider cannot be resolved through dependency injection.
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProviderNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderNotFoundException"/> class.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        public ProviderNotFoundException(string providerName)
            : base($"The configured provider '{providerName}' was not found. Did you forget to register providers in your Startup.ConfigureServices?")
        {
        }
    }
}
