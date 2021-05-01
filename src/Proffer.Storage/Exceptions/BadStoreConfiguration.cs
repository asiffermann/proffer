namespace Proffer.Storage.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Thrown when a store was not properly configured.
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadStoreConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        public BadStoreConfiguration(string storeName) 
            : base($"The store '{storeName}' was not properly configured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="details">The details.</param>
        public BadStoreConfiguration(string storeName, string details)
            : base($"The store '{storeName}' was not properly configured. {details}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadStoreConfiguration"/> class.
        /// </summary>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="errors">The errors.</param>
        public BadStoreConfiguration(string storeName, IEnumerable<Configuration.IOptionError> errors)
            : this(storeName, string.Join(" | ", errors.Select(e => e.ErrorMessage)))
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public IEnumerable<Configuration.IOptionError> Errors { get; }
    }
}
