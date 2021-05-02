namespace Proffer.Storage.Tests.Stubs.Configuration
{
    using Proffer.Storage.Configuration;

    public class StubScopedStoreOptions : StubStoreOptions, IScopedStoreOptions
    {
        public string FolderNameFormat => "Stub-{0}";
    }
}
