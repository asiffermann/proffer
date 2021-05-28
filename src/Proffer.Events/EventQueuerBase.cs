namespace Proffer.Events
{
    /// <summary>
    /// An abstract base typed event queuer
    /// </summary>
    public abstract class EventQueuerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventQueuerBase"/> class.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="eventQueuerFactory">The event queuer factory.</param>
        public EventQueuerBase(string queueName, IEventFactory eventQueuerFactory)
        {
            this.Queuer = eventQueuerFactory.GetQueuer(queueName);
        }

        /// <summary>
        /// Gets the queuer.
        /// </summary>
        public IEventQueuer Queuer { get; }

    }
}
