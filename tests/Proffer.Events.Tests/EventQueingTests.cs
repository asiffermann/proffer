namespace Proffer.Events.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Events.Tests.Stubs;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Events))]
    [Feature(nameof(IEventQueuer))]
    public class EventQueingTests
    {
        [Fact]
        public void Should_Queue_Event()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            StubEvent @event = new StubEvent("TESTEVENT-01");
            IEventQueuer eventQueuer = eventFactory.GetQueuer("EmailQueue");
            eventQueuer.QueueEvent(@event);
            IEnumerable<EventBase> queuedUncommitedEvents = eventQueuer.Flush();

            Assert.Contains(queuedUncommitedEvents, (queuedEvent) => queuedEvent == @event);
        }

        [Fact]
        public void Should_Not_Queue_Duplicate_Event()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            StubEvent @event = new StubEvent("TESTEVENT-01");
            IEventQueuer eventQueuer = eventFactory.GetQueuer("EmailQueue");
            eventQueuer.QueueEvent(@event);
            IEnumerable<EventBase> queuedUncommitedEvents = eventQueuer.Flush();

            Assert.Single(queuedUncommitedEvents);
            Assert.Contains(queuedUncommitedEvents, (queuedEvent) => queuedEvent == @event);
        }
    }
}
