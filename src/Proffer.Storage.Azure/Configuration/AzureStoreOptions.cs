namespace Proffer.Storage.Azure.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Options for an <see cref="AzureStore"/>.
    /// </summary>
    /// <seealso cref="StoreOptions" />
    public class AzureStoreOptions : StoreOptions
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the connection string to reference.
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// Validates the options.
        /// </summary>
        /// <param name="throwOnError">If set to <c>true</c>, throws an exception when the validation fails with any <see cref="IOptionError" />.</param>
        /// <returns>
        /// The <see cref="IOptionError" /> returned by the validation, if any.
        /// </returns>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        public override IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            IEnumerable<IOptionError> baseErrors = base.Validate(throwOnError);
            var optionErrors = new List<OptionError>();

            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                this.PushMissingPropertyError(optionErrors, nameof(this.ConnectionString));
            }

            IEnumerable<IOptionError> finalErrors = baseErrors.Concat(optionErrors);
            if (throwOnError && finalErrors.Any())
            {
                throw new Exceptions.BadStoreConfiguration(this.Name, finalErrors);
            }

            return finalErrors;
        }
    }
}
