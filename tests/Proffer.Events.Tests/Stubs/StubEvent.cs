namespace Proffer.Events.Tests.Stubs
{
    public class StubEvent : EventBase
    {
        public StubEvent(string id)
            : base()
        {
            this.Id = id;
        }
        public override string Key => this.GetType().ToString()
            + this.Id;

        public string Id { get; set; }
    }
}
