namespace Proffer.Storage.FileSystem.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(FileSystemCollection))]
    public class FileSystemCollection : ICollectionFixture<FileSystemFixture> { }
}
