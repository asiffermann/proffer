namespace Proffer.Storage.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a scoped store was not properly configured.
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadScopedStoreConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadScopedStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        public BadScopedStoreConfiguration(string storeName) 
            : base($"The scoped store '{storeName}' was not properly configured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadScopedStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="details">The details.</param>
        public BadScopedStoreConfiguration(string storeName, string details)
            : base($"The scoped store '{storeName}' was not properly configured. {details}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadScopedStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="details">The details.</param>
        /// <param name="innerException">The inner exception.</param>
        public BadScopedStoreConfiguration(string storeName, string details, Exception innerException)
            : base($"The scoped store '{storeName}' was not properly configured. {details}", innerException)
        {
        }
    }
}
