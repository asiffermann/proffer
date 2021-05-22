# IStorageFactory

Namespace: Proffer.Storage

Builds [IStore](./proffer.storage.istore.md) from configured [IStorageProvider](./proffer.storage.istorageprovider.md).

```csharp
public interface IStorageFactory
```

## Methods

### **GetStore(String, IStoreOptions)**

Gets a store with specific options.

```csharp
IStore GetStore(string storeName, IStoreOptions configuration)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`configuration` [IStoreOptions](./proffer.storage.configuration.istoreoptions.md)<br>
The store options.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

[ProviderNotFoundException](./proffer.storage.exceptions.providernotfoundexception.md)<br>

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **GetStore(String)**

Gets a store from configured options.

```csharp
IStore GetStore(string storeName)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

[ProviderNotFoundException](./proffer.storage.exceptions.providernotfoundexception.md)<br>

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **GetScopedStore(String, Object[])**

Gets a scoped store from configured options.

```csharp
IStore GetScopedStore(string storeName, Object[] args)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the scoped store.

`args` [Object[]](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The arguments to apply to the scoped store name format.

#### Returns

[IStore](./proffer.storage.istore.md)<br>

            A configured .

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

[ProviderNotFoundException](./proffer.storage.exceptions.providernotfoundexception.md)<br>

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

[BadScopedStoreConfiguration](./proffer.storage.exceptions.badscopedstoreconfiguration.md)<br>

### **TryGetStore(String, IStore&)**

Gets a store from configured options.

```csharp
bool TryGetStore(string storeName, IStore& store)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`store` [IStore&](./proffer.storage.istore&.md)<br>
When this method returns, contains the store associated with the specified name, if it is found in the ; otherwise, null. This parameter is passed uninitialized.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the store was configured and built from its provider; otherwise, false.

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

[ProviderNotFoundException](./proffer.storage.exceptions.providernotfoundexception.md)<br>

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **TryGetStore(String, IStore&, String)**

Gets a store from configured options.

```csharp
bool TryGetStore(string storeName, IStore& store, string providerName)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`store` [IStore&](./proffer.storage.istore&.md)<br>
When this method returns, contains the store associated with the specified name, if it is found in the ; otherwise, null. This parameter is passed uninitialized.

`providerName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The explicit provider name from which the store should be built.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the store was configured and built from its provider; otherwise, false.

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

[ProviderNotFoundException](./proffer.storage.exceptions.providernotfoundexception.md)<br>

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>
