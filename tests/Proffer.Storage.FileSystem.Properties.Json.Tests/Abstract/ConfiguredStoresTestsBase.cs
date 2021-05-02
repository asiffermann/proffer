namespace Proffer.Storage.FileSystem.Properties.Json.Tests.Abstract
{
    using Xunit;

    public abstract class ConfiguredStoresTestsBase
    {
        public static TheoryData<string> ConfiguredStoreNames
            => new()
            {
                { "Basic" },
                { "NamedProvider" },
                { "CustomRootPathNamedProvider" },
                { "CustomFolderName" },
                { "CustomRootPath" },
                { "CustomRootPathWithFolder" }
            };

        public static TheoryData<string> ConfiguredScopedStoreNames
            => new()
            {
                { "ScopedBasic" },
                { "ScopedNamedProvider" },
                { "ScopedCustomRootPathNamedProvider" },
                { "ScopedCustomRootPath" },
            };
    }
}

