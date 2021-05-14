namespace Proffer.Events.Configuration.Queue
{
    using System.Collections.Generic;
    using Proffer.Configuration;

    /// <summary>
    /// An interface that's define generic options for <see cref="">
    /// </summary>
    /// <seealso cref="INamedElementOptions" />
    public interface IQueueOptions : INamedElementOptions
    {
        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        string ProviderType { get; set; }

        /// <summary>
        /// Validates the specified throw on error.
        /// </summary>
        /// <param name="throwOnError">if set to <c>true</c> throws an exception when the validation fails with any <see cref="IOptionError"/>.</param>
        /// <returns>The <see cref="IOptionError"/> returned by the validation</returns>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        IEnumerable<IOptionError> Validate(bool throwOnError = true);
    }
}
