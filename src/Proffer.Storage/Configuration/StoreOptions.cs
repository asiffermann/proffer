namespace Proffer.Storage.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Proffer.Configuration;

    /// <summary>
    /// Generic options for an <see cref="IStore"/>.
    /// </summary>
    /// <seealso cref="IStoreOptions" />
    public class StoreOptions : IStoreOptions
    {
        private const string MissingPropertyErrorMessage = "{0} should be defined.";

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        public string ProviderType { get; set; }

        /// <summary>
        /// Gets or sets the access level.
        /// </summary>
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Validates the options.
        /// </summary>
        /// <param name="throwOnError">If set to <c>true</c>, throws an exception when the validation fails with any <see cref="IOptionError" />.</param>
        /// <returns>
        /// The <see cref="IOptionError" /> returned by the validation, if any.
        /// </returns>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        public virtual IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            var optionErrors = new List<OptionError>();

            if (string.IsNullOrEmpty(this.Name))
            {
                this.PushMissingPropertyError(optionErrors, nameof(this.Name));
            }

            if (string.IsNullOrEmpty(this.ProviderName) && string.IsNullOrEmpty(this.ProviderType))
            {
                optionErrors.Add(new OptionError
                {
                    PropertyName = "Provider",
                    ErrorMessage = $"You should set either a {nameof(this.ProviderType)} or a {nameof(this.ProviderName)} for each Store."
                });
            }

            if (string.IsNullOrEmpty(this.FolderName))
            {
                this.PushMissingPropertyError(optionErrors, nameof(this.FolderName));
            }

            if (throwOnError && optionErrors.Any())
            {
                throw new Exceptions.BadStoreConfiguration(this.Name, optionErrors);
            }

            return optionErrors;
        }

        /// <summary>
        /// Pushes a missing property <see cref="IOptionError"/> to the list or errors.
        /// </summary>
        /// <param name="optionErrors">The option errors.</param>
        /// <param name="propertyName">The name of the missing property.</param>
        protected void PushMissingPropertyError(List<OptionError> optionErrors, string propertyName)
        {
            optionErrors.Add(new OptionError
            {
                PropertyName = propertyName,
                ErrorMessage = string.Format(MissingPropertyErrorMessage, propertyName)
            });
        }
    }
}
