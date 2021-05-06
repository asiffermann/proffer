namespace Proffer.Storage.Azure.Blobs.Tests.Stubs
{
    using System;
    using Proffer.Storage.Azure.Blobs.Configuration;
    using Storage.Configuration;

    public class AzureBlobsStoreOptionsStub : AzureBlobsStoreOptions
    {
        public AzureBlobsStoreOptionsStub()
        {
            this.Name = "GenericStore";
            this.ProviderType = "Azure";
            this.AccessLevel = ( DateTime.UtcNow.Day % 2 == 0 ) ? AccessLevel.Public : AccessLevel.Private;
        }
    }
}
