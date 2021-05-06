namespace Proffer.Configuration
{
    /// <summary>
    /// A generic error reported from the options validation.
    /// </summary>
    /// <seealso cref="IOptionError" />
    public class OptionError : IOptionError
    {
        /// <summary>
        /// Gets the name of the faulted property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
