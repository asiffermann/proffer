namespace Proffer.Events.AzureStorage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Azure.Storage.Queues;
    using Proffer.Events.AzureStorage.Configuration;
    using Proffer.Events.AzureStorage.Internal;

    /// <summary>
    /// An Azure stroge event queuer
    /// </summary>
    /// <seealso cref="IEventQueuer" />
    public class AzureStorageEventQueuer : IEventQueuer
    {
        private readonly AzureStorageQueueOptions queueOptions;
        private readonly QueueServiceClient queueService;
        private readonly Queue<EventBase> queue = new Queue<EventBase>();

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.queueOptions.Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageEventQueuer"/> class.
        /// </summary>
        /// <param name="queueOptions">The queue options.</param>
        public AzureStorageEventQueuer(AzureStorageQueueOptions queueOptions)
        {
            this.queueOptions = queueOptions;
            this.queueService = new QueueServiceClient(queueOptions.ConnectionString);
        }

        /// <summary>
        /// Queues the event. (uncommited pushed events)
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
        /// Commits the events to the queue storage asynchronous.
        /// </summary>
        public async Task CommitAsync()
        {
            await this.queueService.EnsureQueueIsCreatedAsync(this.queueOptions.Name);
            QueueClient queueClient = this.queueService.GetQueueClient(this.queueOptions.Name);

            var sendMessagesTaskList = new List<Task>();
            foreach (EventBase @event in this.queue)
            {
                sendMessagesTaskList.Add(queueClient.SendMessageAsync(JsonSerializer.Serialize(@event)));
            }

            this.queue.Clear();
            await Task.WhenAll(sendMessagesTaskList);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public async void Dispose()
        {
            await this.CommitAsync();
        }

        /// <summary>
        /// Flushes the uncommited message.
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
