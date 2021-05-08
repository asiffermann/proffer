namespace Proffer.Storage.FileSystem.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(FileSystemTestCollection))]
    public class FileSystemTestCollection : ICollectionFixture<FileSystemFixture> { }
}
