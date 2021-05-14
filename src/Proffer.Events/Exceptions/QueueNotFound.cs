namespace Proffer.Events.Exceptions
{
    using System;

    /// <summary>
    /// An exception thrown when the named queue cannot be found in the configuration
    /// </summary>
    /// <seealso cref="Exception" />
    public class QueueNotFound : Exception
    {
        public QueueNotFound(string queueName)
            : base($"The queue '{queueName}' was not found. Did you configure it properly in appsettings.json ?")
        {
        }
    }
}
