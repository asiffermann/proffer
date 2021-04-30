namespace Providers.Storage.Configuration
{
    /// <summary>
    /// An error reported from the options validation.
    /// </summary>
    public interface IOptionError
    {
        /// <summary>
        /// Gets the name of the faulted property.
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        string ErrorMessage { get; }
    }
}
