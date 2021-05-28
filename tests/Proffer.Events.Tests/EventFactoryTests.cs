namespace Proffer.Events.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Events.Exceptions;
    using Proffer.Events.Tests.Stubs;
    using Proffer.Events.Tests.Stubs.Configuration;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Events))]
    [Feature(nameof(IEventFactory))]
    public class EventFactoryTests
    {
        [Fact]
        public void Should_Get_Queuer_Without_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            IEventQueuer queuer = eventFactory.GetQueuer("EmailQueue");

            Assert.NotNull(queuer);
        }

        [Fact]
        public void Should_Get_Unknown_Queuer_With_Exception()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.Throws<QueueNotFound>(() => eventFactory.GetQueuer("UnknownQueue"));
        }

        [Fact]
        public void Should_Get_Queuer_With_Queue_Options()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();
            IOptions<StubOptions> options = fixture.Services.GetRequiredService<IOptions<StubOptions>>();

            options.Value.QueueOptions.TryGetValue("EmailQueue", out StubQueueOptions queueOptions);
            Assert.NotNull(eventFactory.GetQueuer("EmailQueue", queueOptions));
        }

        [Fact]
        public void Should_Throw_Exception_Get_Queuer_With_Invalid_Queue_Options()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();
            IOptions<StubOptions> options = fixture.Services.GetRequiredService<IOptions<StubOptions>>();

            options.Value.QueueOptions.TryGetValue("EmailQueue", out StubQueueOptions queueOptions);
            queueOptions.
            queueOptions.ProviderType = "Unknown";
            Assert.Throws<ProviderNotFound>(() => eventFactory.GetQueuer("EmailQueue", queueOptions));
            queueOptions.ProviderType = null;
            queueOptions.ProviderName = "Unknown";
            Assert.Throws<BadProviderConfiguration>(() => eventFactory.GetQueuer("EmailQueue", queueOptions));
            queueOptions.ProviderName = null;
            Assert.Throws<BadQueueConfiguration>(() => eventFactory.GetQueuer("EmailQueue", queueOptions));
        }

        [Fact]
        public void Should_Try_Get_Queuer_Without_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.True(eventFactory.TryGetQueuer("EmailQueue", out IEventQueuer queuer));
            Assert.NotNull(queuer);
        }

        [Fact]
        public void Should_Try_Get_Queuer_With_Unknown_QueueName()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.False(eventFactory.TryGetQueuer("UnknownQueue", out IEventQueuer queuer));
            Assert.Null(queuer);
        }

        [Fact]
        public void Should_Try_Get_Queuer_With_Unknown_Provider()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.False(eventFactory.TryGetQueuer("EmailQueue", out IEventQueuer queuer, "BadProvider"));
            Assert.Null(queuer);
        }

        [Fact]
        public void Should_Try_Get_Queuer_With_Unknown_Queue()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration.GetSection("Event")).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.False(eventFactory.TryGetQueuer("UnknownQueue", out IEventQueuer queuer, "StubProvider"));
            Assert.Null(queuer);
        }

        [Fact]
        public void Should_Try_Get_Queuer_With_Specific_Provider()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IEventFactory eventFactory = fixture.Services.GetRequiredService<IEventFactory>();

            Assert.True(eventFactory.TryGetQueuer("EmailQueue", out IEventQueuer queuer, "Stub"));
            Assert.NotNull(queuer);
        }
    }
}
