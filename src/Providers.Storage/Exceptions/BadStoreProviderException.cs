namespace Providers.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a store was not configured with a specific provider.
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadStoreProviderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadStoreProviderException"/> class.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <param name="storeName">The name of the store.</param>
        public BadStoreProviderException(string providerName, string storeName) 
            : base($"The store '{storeName}' was not configured with the provider '{providerName}'. Unable to build it.")
        {
        }
    }
}
