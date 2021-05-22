# ScopedStoreOptions

Namespace: Proffer.Storage.Configuration

Generic options for a scoped [IStore](./proffer.storage.istore.md).

```csharp
public class ScopedStoreOptions : StoreOptions, IStoreOptions, Proffer.Configuration.INamedElementOptions, IScopedStoreOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StoreOptions](./proffer.storage.configuration.storeoptions.md) → [ScopedStoreOptions](./proffer.storage.configuration.scopedstoreoptions.md)<br>
Implements [IStoreOptions](./proffer.storage.configuration.istoreoptions.md), INamedElementOptions, [IScopedStoreOptions](./proffer.storage.configuration.iscopedstoreoptions.md)

## Properties

### **FolderNameFormat**

Gets the folder name format.

```csharp
public string FolderNameFormat { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

Gets or sets the name.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderName**

Gets or sets the name of the provider.

```csharp
public string ProviderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderType**

Gets or sets the type of the provider.

```csharp
public string ProviderType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AccessLevel**

Gets or sets the access level.

```csharp
public AccessLevel AccessLevel { get; set; }
```

#### Property Value

[AccessLevel](./proffer.storage.configuration.accesslevel.md)<br>

### **FolderName**

Gets or sets the name of the folder.

```csharp
public string FolderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ScopedStoreOptions()**



```csharp
public ScopedStoreOptions()
```
