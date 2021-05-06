namespace Proffer.Storage.Azure.Blobs.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(AzureBlobsCollection))]
    public class AzureBlobsCollection : ICollectionFixture<AzureBlobsFixture> { }
}
