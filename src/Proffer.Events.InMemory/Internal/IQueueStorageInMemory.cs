namespace Proffer.Events.InMemory.Internal
{
    /// <summary>
    /// An interface for in memory queue definition. 
    /// </summary>
    public interface IQueueStorageInMemory
    {
        /// <summary>
        /// Adds the specified event base to the queue.
        /// </summary>
        /// <param name="eventBase">The event base.</param>
        void Add(EventBase eventBase);

        /// <summary>
        /// Gets the next event from queue.
        /// </summary>
        /// <returns></returns>
        EventBase GetEvent();
    }
}
