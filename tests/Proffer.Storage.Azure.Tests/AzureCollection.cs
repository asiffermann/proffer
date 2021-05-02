namespace Proffer.Storage.Azure.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(AzureCollection))]
    public class AzureCollection : ICollectionFixture<AzureFixture> { }
}
