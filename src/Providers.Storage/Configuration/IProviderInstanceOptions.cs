namespace Providers.Storage.Configuration
{
    public interface IProviderInstanceOptions : INamedElementOptions
    {
        string Type { get; }
    }
}
