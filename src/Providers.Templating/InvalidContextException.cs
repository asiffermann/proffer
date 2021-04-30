namespace Providers.Templating
{
    using System;

    public class InvalidContextException : Exception
    {
        public InvalidContextException(Exception innerException) : base("Invalid template generation context.", innerException)
        {
        }
    }
}
