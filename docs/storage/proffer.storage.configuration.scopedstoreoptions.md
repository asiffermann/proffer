# ScopedStoreOptions

Namespace: Proffer.Storage.Configuration

Generic options for a scoped [IStore](./proffer.storage.istore).

```csharp
public class ScopedStoreOptions : StoreOptions, IStoreOptions, Proffer.Configuration.INamedElementOptions, IScopedStoreOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StoreOptions](./proffer.storage.configuration.storeoptions) → [ScopedStoreOptions](./proffer.storage.configuration.scopedstoreoptions)<br>
Implements [IStoreOptions](./proffer.storage.configuration.istoreoptions), INamedElementOptions, [IScopedStoreOptions](./proffer.storage.configuration.iscopedstoreoptions)

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

[AccessLevel](./proffer.storage.configuration.accesslevel)<br>

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
