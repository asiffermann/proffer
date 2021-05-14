namespace Proffer.Events.Configuration.Queue
{
    using System.Collections.Generic;
    using Proffer.Configuration;

    /// <summary>
    /// Generic options for an Queue
    /// </summary>
    /// <seealso cref="IQueueOptions" />
    public class QueueOptions : IQueueOptions
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
        /// Validates the specified throw on error.
        /// </summary>
        /// <param name="throwOnError">if set to <c>true</c> throws an exception when the validation fails with any <see cref="IOptionError" />.</param>
        /// <returns>
        /// The <see cref="IOptionError" /> returned by the validation
        /// </returns>
        /// <exception cref="Exceptions.BadQueueConfiguration"></exception>
        public IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            var optionsErrors = new List<OptionError>();

            if (string.IsNullOrEmpty(this.Name))
            {
                this.PushMissingPropertyError(optionsErrors, nameof(this.Name));
            }

            if (string.IsNullOrEmpty(this.ProviderName) && string.IsNullOrEmpty(this.ProviderType))
            {
                optionsErrors.Add(new OptionError
                {
                    PropertyName = "Providers",
                    ErrorMessage = $"You should set either a {nameof(this.ProviderType)} or a {nameof(this.ProviderName)} for each Queue."
                });
            }

            return optionsErrors;
        }

        /// <summary>
        /// Pushes the missing property <see cref="IOptionError"/> to the list or errors.
        /// </summary>
        /// <param name="optionErrors">The option errors.</param>
        /// <param name="propertyName">Name of the property.</param>
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
