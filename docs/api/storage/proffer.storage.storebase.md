# StoreBase

Namespace: Proffer.Storage

Abstract base typed store.

```csharp
public abstract class StoreBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StoreBase](./proffer.storage.storebase.md)

## Properties

### **Store**

Gets the store.

```csharp
public IStore Store { get; }
```

#### Property Value

[IStore](./proffer.storage.istore.md)<br>

## Constructors

### **StoreBase(String, IStorageFactory)**

Initializes a new instance of the [StoreBase](./proffer.storage.storebase.md) class.

```csharp
public StoreBase(string storeName, IStorageFactory storageFactory)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storageFactory` [IStorageFactory](./proffer.storage.istoragefactory.md)<br>
The storage factory.
