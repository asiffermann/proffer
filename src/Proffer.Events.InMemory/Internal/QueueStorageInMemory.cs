namespace Proffer.Events.InMemory.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// An in memory queue
    /// </summary>
    /// <seealso cref="IQueueStorageInMemory" />
    public class QueueStorageInMemory : IQueueStorageInMemory
    {
        /// <summary>
        /// The queue
        /// </summary>
        private readonly Queue<EventBase> queue = new Queue<EventBase>();

        /// <summary>
        /// Adds the specified event base to the queue.
        /// </summary>
        /// <param name="eventBase">The event base.</param>
        public void Add(EventBase eventBase)
        {
            this.queue.Enqueue(eventBase);
        }

        /// <summary>
        /// Gets the next event from queue.
        /// </summary>
        /// <returns>
        ///     The next <see cref="EventBase"/> from the <seealso cref="Queue{EventBase}"/> 
        /// </returns>
        public EventBase GetEvent()
        {
            return this.queue.Dequeue();
        }
    }
}
