namespace Proffer.Storage.Tests.Stubs
{
    using Proffer.Storage.Configuration;

    public class GenericStoreOptionsStub : StoreOptions
    {
        public GenericStoreOptionsStub()
        {
            this.Name = "GenericStore";
            this.ProviderType = "Stub";
        }
    }
}
