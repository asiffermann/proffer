namespace Proffer.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A disposable event queuer
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IEventQueuer : IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Queues the event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The event.</param>
        void QueueEvent<TEvent>(TEvent @event) where TEvent : EventBase;

        /// <summary>
        /// Commits the <see cref="EventBase"/> asynchronous.
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// Flushes this queuer instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<EventBase> Flush();
    }
}
