namespace Providers.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a store was not found in the configuration.
    /// </summary>
    /// <seealso cref="Exception" />
    public class StoreNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreNotFoundException"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        public StoreNotFoundException(string storeName)
            : base($"The configured store '{storeName}' was not found. Did you configure it properly in your appsettings.json?")
        {
        }
    }
}
