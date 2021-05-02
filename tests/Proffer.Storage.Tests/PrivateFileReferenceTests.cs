namespace Proffer.Storage.Tests
{
    using Proffer.Storage.Internal;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(PrivateFileReference))]
    public class PrivateFileReferenceTests
    {
        [Fact]
        public void Should_NormalizePath_With_Backslash()
        {
            string path = "SubDirectory\\TextFile2.txt";
            var fileReference = new PrivateFileReference(path);

            Assert.Equal("SubDirectory/TextFile2.txt", fileReference.Path);
        }

        [Fact]
        public void Should_NormalizePath_With_StartingSlash()
        {
            string path = "/SubDirectory/TextFile2.txt";
            var fileReference = new PrivateFileReference(path);

            Assert.Equal("SubDirectory/TextFile2.txt", fileReference.Path);
        }
    }
}
