namespace Proffer.Events
{
    using System.Threading.Tasks;

    /// <summary>
    /// An interface that define an event receiver.
    /// </summary>
    /// <remarks>
    /// Event recievers, execute the specific <see cref="IEventHandler{TEvent}"/> for the given <seealso cref="EventBase"/>  
    /// </remarks>
    public interface IEventReceiver
    {
        /// <summary>
        /// Execute the handler the given event  the <see cref="EventBase"/> asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The event.</param>
        Task ReceiveAsync<TEvent>(TEvent @event) where TEvent : EventBase;
    }
}
