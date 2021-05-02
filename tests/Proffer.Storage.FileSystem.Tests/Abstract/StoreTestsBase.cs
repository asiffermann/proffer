namespace Proffer.Storage.FileSystem.Tests.Abstract
{
    using Xunit;

    public abstract class StoreTestsBase
    {
        public static TheoryData<string, FileSystemFixture> ConfiguredStoreNames
        {
            get
            {
                var fixture = new FileSystemFixture();
                return new TheoryData<string, FileSystemFixture>
                {
                    { "Basic", fixture },
                    { "NamedProvider", fixture },
                    { "CustomRootPathNamedProvider", fixture },
                    { "CustomFolderName", fixture },
                    { "CustomRootPath", fixture },
                    { "CustomRootPathWithFolder", fixture }
                };
            }
        }

        public static TheoryData<string, FileSystemFixture> ConfiguredScopedStoreNames
        {
            get
            {
                var fixture = new FileSystemFixture();
                return new TheoryData<string, FileSystemFixture>
                {
                    { "ScopedBasic", fixture },
                    { "ScopedNamedProvider", fixture },
                    { "ScopedCustomRootPathNamedProvider", fixture },
                    { "ScopedCustomRootPath", fixture },
                };
            }
        }
    }
}

