namespace Proffer.Events.Exceptions
{
    using System;

    /// <summary>
    /// An exception thrown when the named provider cannot be found in the configuration
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProviderNotFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderNotFound"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public ProviderNotFound(string providerName)
            : base($"The provider '{providerName}' was not found.")
        {
        }
    }
}
