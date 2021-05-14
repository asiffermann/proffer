namespace Proffer.Events.Exceptions
{
    using System;

    /// <summary>
    /// An exception thrown when the queue configuration is malformed
    /// </summary>
    /// <seealso cref="Exception" />
    public class BadQueueConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadQueueConfiguration" /> class.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        public BadQueueConfiguration(string queueName, Exception innerException = null)
            : base($"The Queue '{queueName}' was not properly configured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadQueueConfiguration"/> class.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="details">The details.</param>
        public BadQueueConfiguration(string queueName, string details)
            : base($"The Queue '{queueName}' was not properly configured. {details}")
        {
        }
    }
}
