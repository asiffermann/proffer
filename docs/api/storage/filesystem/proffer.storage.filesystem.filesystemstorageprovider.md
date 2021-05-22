# FileSystemStorageProvider

Namespace: Proffer.Storage.FileSystem

A provider to handle and build file stores pointing on a File System directory.

```csharp
public class FileSystemStorageProvider : Proffer.Storage.Internal.StorageProviderBase`4[[Proffer.Storage.FileSystem.Configuration.FileSystemParsedOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.FileSystem.Configuration.FileSystemProviderOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.FileSystem.Configuration.FileSystemStoreOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.FileSystem.Configuration.FileSystemScopedStoreOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Proffer.Storage.IStorageProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StorageProviderBase&lt;FileSystemParsedOptions, FileSystemProviderOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions&gt; → [FileSystemStorageProvider](./proffer.storage.filesystem.filesystemstorageprovider)<br>
Implements IStorageProvider

## Fields

### **ProviderName**

The [FileSystemStorageProvider](./proffer.storage.filesystem.filesystemstorageprovider) name.

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

### **FileSystemStorageProvider(IOptions&lt;FileSystemParsedOptions&gt;, IPublicUrlProvider, IExtendedPropertiesProvider)**

Initializes a new instance of the [FileSystemStorageProvider](./proffer.storage.filesystem.filesystemstorageprovider) class.

```csharp
public FileSystemStorageProvider(IOptions<FileSystemParsedOptions> options, IPublicUrlProvider publicUrlProvider, IExtendedPropertiesProvider extendedPropertiesProvider)
```

#### Parameters

`options` IOptions&lt;FileSystemParsedOptions&gt;<br>
The options.

`publicUrlProvider` [IPublicUrlProvider](./proffer.storage.filesystem.ipublicurlprovider)<br>
The public URL provider.

`extendedPropertiesProvider` [IExtendedPropertiesProvider](./proffer.storage.filesystem.iextendedpropertiesprovider)<br>
The extended properties provider.

## Methods

### **BuildStoreInternal(String, FileSystemStoreOptions)**

Provider-specific build of a store with specific options.

```csharp
protected IStore BuildStoreInternal(string storeName, FileSystemStoreOptions storeOptions)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storeOptions` [FileSystemStoreOptions](./proffer.storage.filesystem.configuration.filesystemstoreoptions)<br>
The store options.

#### Returns

IStore<br>

            A configured .
