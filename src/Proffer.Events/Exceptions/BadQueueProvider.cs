namespace Proffer.Events.Exceptions
{
    using System;

    public class BadQueueProvider : Exception
    {
        public BadQueueProvider(string providerName, string queueName)
            : base($"The queue '{queueName}' was not configured with the provider '{providerName}'. Unable to build it.")
        {
        }
    }
}
