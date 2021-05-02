namespace Proffer.Storage.Azure.Tests.Stubs
{
    using Storage.Configuration;
    using Proffer.Storage.Azure.Configuration;
    using System;

    public class AzureStoreOptionsStub : AzureStoreOptions
    {
        public AzureStoreOptionsStub()
        {
            this.Name = "GenericStore";
            this.ProviderType = "Azure";
            this.AccessLevel = (DateTime.UtcNow.Day % 2 == 0) ? AccessLevel.Public : AccessLevel.Private;
        }
    }
}
