namespace Proffer.Events.Tests.Stubs
{
    using System;
    using System.Threading.Tasks;

    public class StubEventHandler : IEventHandler<StubEvent>
    {
        public Task ExecuteAsync(StubEvent evenBase) => throw new NotImplementedException();
    }
}
