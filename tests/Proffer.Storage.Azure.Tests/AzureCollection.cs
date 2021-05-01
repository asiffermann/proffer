namespace Proffer.Storage.Azure.Tests
{
    using Proffer.Testing;
    using Xunit;

    [CollectionDefinition(nameof(AzureCollection))]
    public class AzureCollection : ServiceProviderCollection<AzureFixture> { }
}
