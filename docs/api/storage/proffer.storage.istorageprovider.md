# IStorageProvider

Namespace: Proffer.Storage

A provider handles and builds file stores pointing on a particular storage system location.

```csharp
public interface IStorageProvider
```

## Properties

### **Name**

Gets the name of this provider.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **BuildStore(String)**

Builds a store from configured options.

```csharp
IStore BuildStore(string storeName)
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
IStore BuildStore(string storeName, IStoreOptions storeOptions)
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
IStore BuildScopedStore(string storeName, Object[] args)
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
