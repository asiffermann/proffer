namespace Proffer.Testing
{
    using Xunit;

    public class ServiceProviderCollection<TFixture> : ICollectionFixture<TFixture>
        where TFixture : ServiceProviderFixture
    {
    }
}
