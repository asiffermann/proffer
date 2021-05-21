[`< Back`](./)

---

# AzureBlobsStorageProvider

Namespace: Proffer.Storage.Azure.Blobs

A provider to handle and build file stores pointing on an Azure Storage account.

```csharp
public class AzureBlobsStorageProvider : Proffer.Storage.Internal.StorageProviderBase`4[[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsParsedOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsProviderOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsStoreOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsScopedStoreOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Proffer.Storage.IStorageProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StorageProviderBase&lt;AzureBlobsParsedOptions, AzureBlobsProviderOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions&gt; → [AzureBlobsStorageProvider](./proffer.storage.azure.blobs.azureblobsstorageprovider)<br>
Implements IStorageProvider

## Fields

### **ProviderName**

The [AzureBlobsStorageProvider](./proffer.storage.azure.blobs.azureblobsstorageprovider) name.

```csharp
public static string ProviderName;
```

## Properties

### **Name**

Gets the name of this provider.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AzureBlobsStorageProvider(IOptions&lt;AzureBlobsParsedOptions&gt;)**

Initializes a new instance of the [AzureBlobsStorageProvider](./proffer.storage.azure.blobs.azureblobsstorageprovider) class.

```csharp
public AzureBlobsStorageProvider(IOptions<AzureBlobsParsedOptions> options)
```

#### Parameters

`options` IOptions&lt;AzureBlobsParsedOptions&gt;<br>
The options.

## Methods

### **BuildStoreInternal(String, AzureBlobsStoreOptions)**

Provider-specific build of a store with specific options.

```csharp
protected IStore BuildStoreInternal(string storeName, AzureBlobsStoreOptions storeOptions)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storeOptions` [AzureBlobsStoreOptions](./proffer.storage.azure.blobs.configuration.azureblobsstoreoptions)<br>
The store options.

#### Returns

IStore<br>

            A configured .

---

[`< Back`](./)
