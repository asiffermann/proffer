namespace Proffer.Events
{
    using Proffer.Events.Configuration.Queue;

    /// <summary>
    /// An event queuer factory 
    /// </summary>
    public interface IEventFactory
    {
        /// <summary>
        /// Gets the queuer.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        IEventQueuer GetQueuer(string queueName, IQueueOptions configuration);

        /// <summary>
        /// Gets the queuer.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns></returns>
        IEventQueuer GetQueuer(string queueName);

        /// <summary>
        /// Tries the get queuer.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="queuer">The queuer.</param>
        /// <returns></returns>
        bool TryGetQueuer(string queueName, out IEventQueuer queuer);

        /// <summary>
        /// Tries the get queuer.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="queuer">The queuer.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        bool TryGetQueuer(string queueName, out IEventQueuer queuer, string provider);
    }
}
