namespace Proffer.Email.Tests.Stubs
{
    public class StubEmailProviderType : IEmailProviderType
    {
        public string Name => "Stub";

        public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
            => new StubEmailProvider();
    }
}
