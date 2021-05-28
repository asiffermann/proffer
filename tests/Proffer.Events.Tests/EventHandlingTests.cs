namespace Proffer.Events.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Events.Tests.Stubs;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Events))]
    [Feature(nameof(IEventReceiver))]
    public class EventHandlingTests
    {
        [Fact]
        public async Task Should_Recieve_EventAsync()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) =>
                {
                    sp.AddEvent(f.Configuration).AddStubQueue().AddEventReceiver();
                    sp.AddTransient<IEventHandler<StubEvent>, StubEventHandler>();
                });

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();
            StubEvent @event = new StubEvent("TESTEVENT-01");

            IEventReceiver eventReceiver = fixture.Services.GetRequiredService<IEventReceiver>();
            await Assert.ThrowsAsync<InvalidOperationException>(() => eventReceiver.ReceiveAsync(@event));
        }
    }
}
