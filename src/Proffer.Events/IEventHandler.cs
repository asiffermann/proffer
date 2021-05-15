namespace Proffer.Events
{
    using System.Threading.Tasks;

    /// <summary>
    /// A generic <see cref="EventBase"/> handler
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IEventHandler<TEvent> where TEvent : EventBase
    {
        /// <summary>
        /// Handle the given <see cref="EventBase"/>.
        /// </summary>
        /// <param name="evenBase">The even base.</param>
        /// <returns></returns>
        Task ExecuteAsync(TEvent evenBase);
    }
}
