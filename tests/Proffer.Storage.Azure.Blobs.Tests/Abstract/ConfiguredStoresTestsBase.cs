namespace Proffer.Storage.Azure.Blobs.Tests.Abstract
{
    using Xunit;

    public abstract class ConfiguredStoresTestsBase
    {
        public static TheoryData<string> ConfiguredStoreNames
            => new()
            {
                { "CustomConnectionStringProvider" },
                { "CustomConnectionString" },
                { "ReferenceConnectionStringProvider" },
                { "ReferenceConnectionString" },
            };

        public static TheoryData<string> ConfiguredScopedStoreNames
            => new()
            {
                { "ScopedCustomConnectionStringProvider" },
                { "ScopedCustomConnectionString" },
                { "ScopedReferenceConnectionStringProvider" },
                { "ScopedReferenceConnectionString" },
            };
    }
}

