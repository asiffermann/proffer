namespace Proffer.Events.InMemory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Proffer.Events.Configuration.Queue;
    using Proffer.Events.InMemory.Internal;

    /// <summary>
    /// An in memory event queuer 
    /// </summary>
    /// <seealso cref="IEventQueuer" />
    public class InMemoryEventQueuer : IEventQueuer
    {
        private readonly IQueueOptions queueOptions;
        private readonly Queue<EventBase> queue = new Queue<EventBase>();
        private readonly IQueueStorageInMemory storedQueue;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.queueOptions.Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEventQueuer"/> class.
        /// </summary>
        /// <param name="storedQueue">The stored queue.</param>
        /// <param name="queueOptions">The queue options.</param>
        public InMemoryEventQueuer(IQueueStorageInMemory storedQueue, IQueueOptions queueOptions)
        {
            this.storedQueue = storedQueue;
            this.queueOptions = queueOptions;
        }

        /// <summary>
        /// Queues the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The event.</param>
        public void QueueEvent<TEvent>(TEvent @event) where TEvent : EventBase
        {
            if (!this.queue.Any(e => e == @event))
            {
                this.queue.Enqueue(@event);
            }
        }

        /// <summary>
        /// Commits the <see cref="EventBase" /> asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync()
        {
            foreach (EventBase eventBase in this.queue)
            {
                this.storedQueue.Add(eventBase);
            }

            this.queue.Clear();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public async void Dispose()
        {
            await this.CommitAsync();
        }

        /// <summary>
        /// Flushes the queue.
        /// </summary>
        /// <returns>
        ///     All the events present in the queue when you call this method
        /// </returns>
        public IEnumerable<EventBase> Flush()
        {
            var messagesList = new List<EventBase>();
            foreach (EventBase Event in this.queue)
            {
                messagesList.Add(Event);
            }

            this.queue.Clear();

            return messagesList;
        }
    }
}
