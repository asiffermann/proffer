namespace Providers.Email
{
    public interface IEmailProviderType
    {
        string Name { get; }

        IEmailProvider BuildProvider(IEmailProviderOptions providerOptions);
    }
}