namespace Proffer.Events
{
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// A typed <see cref="IEventReceiver"/>, 
    /// </summary>
    /// <seealso cref="IEventReceiver" />
    public class EventReceiver : IEventReceiver
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReceiver"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public EventReceiver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Receives the asynchronous.
        /// </summary>
        /// <typeparam name="Tevent">The type of the event.</typeparam>
        /// <param name="eventBase">The event base.</param>
        public async Task ReceiveAsync<Tevent>(Tevent eventBase) where Tevent : EventBase
        {
            Type queryType = typeof(IEventHandler<>)
                .MakeGenericType(new Type[] { eventBase.GetType() });

            object getEventHandler = this.serviceProvider.GetService(queryType);

            try
            {
                await (Task)queryType.GetMethod("ExecuteAsync")
                    .Invoke(getEventHandler, new object[] { eventBase });
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("The handler throw an exception", e);
            }

        }
    }
}