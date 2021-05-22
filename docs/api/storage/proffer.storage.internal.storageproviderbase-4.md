# StorageProviderBase&lt;TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;

Namespace: Proffer.Storage.Internal

A base provider to handle and build file stores pointing on a particular storage system location.

```csharp
public abstract class StorageProviderBase<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions> : Proffer.Storage.IStorageProvider
```

#### Type Parameters

`TParsedOptions`<br>
The type of the parsed options.

`TInstanceOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StorageProviderBase&lt;TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;](./proffer.storage.internal.storageproviderbase-4.md)<br>
Implements [IStorageProvider](./proffer.storage.istorageprovider.md)

## Properties

### **Name**

Gets the name of this provider.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **StorageProviderBase(IOptions&lt;TParsedOptions&gt;)**

Initializes a new instance of the [StorageProviderBase&lt;TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;](./proffer.storage.internal.storageproviderbase-4.md) class.

```csharp
public StorageProviderBase(IOptions<TParsedOptions> options)
```

#### Parameters

`options` IOptions&lt;TParsedOptions&gt;<br>
The options.

## Methods

### **BuildStore(String)**

Builds a store from configured options.

```csharp
public IStore BuildStore(string storeName)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

#### Exceptions

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **BuildStore(String, IStoreOptions)**

Builds a store with specific options.

```csharp
public IStore BuildStore(string storeName, IStoreOptions storeOptions)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storeOptions` [IStoreOptions](./proffer.storage.configuration.istoreoptions.md)<br>
The store options.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

### **BuildScopedStore(String, Object[])**

Builds a scoped store from configured options.

```csharp
public IStore BuildScopedStore(string storeName, Object[] args)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`args` [Object[]](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The arguments to apply to the scoped store name format.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

#### Exceptions

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

[BadScopedStoreConfiguration](./proffer.storage.exceptions.badscopedstoreconfiguration.md)<br>

### **BuildStoreInternal(String, TStoreOptions)**

Provider-specific build of a store with specific options.

```csharp
protected abstract IStore BuildStoreInternal(string storeName, TStoreOptions storeOptions)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storeOptions` TStoreOptions<br>
The store options.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .
