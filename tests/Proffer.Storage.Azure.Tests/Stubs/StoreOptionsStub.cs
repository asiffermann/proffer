namespace Proffer.Storage.Azure.Tests.Stubs
{
    using System.Collections.Generic;
    using System.Linq;
    using Proffer.Storage.Configuration;

    public class StoreOptionsStub : IStoreOptions
    {
        public StoreOptionsStub()
        {
            this.Name = "TestStore";
            this.ProviderType = "FileSystem";
        }

        public string ProviderName { get; set; }

        public string ProviderType { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public string FolderName { get; set; }

        public string Name { get; set; }

        public IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            return Enumerable.Empty<IOptionError>();
        }
    }
}
