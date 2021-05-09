namespace Proffer.Storage.Azure.Blobs.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(AzureBlobsTestCollection))]
    public class AzureBlobsTestCollection : ICollectionFixture<AzureBlobsFixture> { }
}
