namespace Proffer.Storage.Azure.Test
{
    using Proffer.Testing;
    using Xunit;

    [CollectionDefinition(nameof(AzureCollection))]
    public class AzureCollection : ServiceProviderCollection<AzureFixture> { }
}
