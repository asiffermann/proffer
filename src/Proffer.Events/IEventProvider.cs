namespace Proffer.Events
{
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// A provider handles and builds queue pointing on a particular storage system location.
    /// </summary>
    public interface IEventProvider
    {
        string Name { get; }

        /// <summary>
        /// Builds the queue provider.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <returns></returns>
        IEventQueuer BuildQueueProvider(string queueName, IQueueOptions storeOptions);

        /// <summary>
        /// Builds the queue provider.
        /// </summary>
        /// <param name="queueName">The name of the queue.</param>
        /// <returns></returns>
        IEventQueuer BuildQueueProvider(string queueName);
    }
}
