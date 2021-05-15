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
        /// <param name="storeName">Name of the store.</param>
        /// <param name="eventQueuerFactory">The event queuer factory.</param>
        public EventQueuerBase(string storeName, IEventFactory eventQueuerFactory)
        {
            this.Queuer = eventQueuerFactory.GetQueuer(storeName);
        }

        /// <summary>
        /// Gets the queuer.
        /// </summary>
        public IEventQueuer Queuer { get; }

    }
}
