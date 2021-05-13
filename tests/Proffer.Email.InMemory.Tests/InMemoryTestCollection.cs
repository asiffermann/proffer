namespace Proffer.Email.InMemory.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(InMemoryTestCollection))]
    public class InMemoryTestCollection : ICollectionFixture<InMemoryFixture> { }
}
