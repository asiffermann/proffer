namespace Proffer.Events.Tests.Stubs
{
    using Microsoft.Extensions.Options;
    using Proffer.Events;
    using Proffer.Events.Internal;
    using Proffer.Events.Tests.Stubs.Configuration;

    public class StubEventProvider : EventsProviderBase<StubOptions, StubProviderOptions, StubQueueOptions>
    {
        public const string ProviderName = "Stub";

        public StubEventProvider(IOptions<StubOptions> options)
            : base(options)
        {
        }

        public override string Name => ProviderName;

        protected override IEventQueuer BuildQueueInternal(string queueName, StubQueueOptions queueOptions) => new StubEventQueuer(queueOptions);
    }
}
