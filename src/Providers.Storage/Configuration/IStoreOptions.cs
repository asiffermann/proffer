namespace Providers.Storage.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// Options for an <see cref="IStore"/>.
    /// </summary>
    /// <seealso cref="INamedElementOptions" />
    public interface IStoreOptions : INamedElementOptions
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
        /// Gets or sets the access level.
        /// </summary>
        AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        string FolderName { get; set; }

        /// <summary>
        /// Validates the options.
        /// </summary>
        /// <param name="throwOnError">If set to <c>true</c>, throws an exception when the validation fails with any <see cref="IOptionError"/>.</param>
        /// <returns>The <see cref="IOptionError"/> returned by the validation, if any.</returns>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        IEnumerable<IOptionError> Validate(bool throwOnError = true);
    }
}
