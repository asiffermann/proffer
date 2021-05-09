namespace Proffer.Templating
{
    using System;

    /// <summary>
    /// Thrown when the generation context is invalid for a template.
    /// </summary>
    /// <seealso cref="Exception" />
    public class InvalidContextException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidContextException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public InvalidContextException(Exception innerException)
            : base("Invalid template generation context.", innerException)
        {
        }
    }
}