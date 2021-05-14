namespace Proffer.Events
{
    using System.Threading.Tasks;
    public interface IEventReciver
    {
        Task ReceiveAsync<TEvent>(TEvent @event) where TEvent : EventBase;
    }
}
