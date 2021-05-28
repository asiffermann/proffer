namespace Proffer.Events.Tests.Stubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Proffer.Events.Tests.Stubs.Configuration;

    public class StubEventQueuer : IEventQueuer
    {
        private readonly StubQueueOptions queueOptions;
        private readonly Queue<EventBase> queue = new Queue<EventBase>();

        public string Name => this.queueOptions.Name;

        public StubEventQueuer(StubQueueOptions queueOptions)
        {
            this.queueOptions = queueOptions;
        }

        public Task CommitAsync() => Task.CompletedTask;

        public void Dispose() { }

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

        public void QueueEvent<TEvent>(TEvent @event) where TEvent : EventBase
        {
            if (!this.queue.Any(e => e == @event))
            {
                this.queue.Enqueue(@event);
            }
        }
    }
}
