[`< Back`](./)

---

# FileSystemScopedStoreOptions

Namespace: Proffer.Storage.FileSystem.Configuration

Options for a scoped [FileSystemStore](./proffer.storage.filesystem.filesystemstore).

```csharp
public class FileSystemScopedStoreOptions : FileSystemStoreOptions, Proffer.Storage.Configuration.IStoreOptions, Proffer.Configuration.INamedElementOptions, Proffer.Storage.Configuration.IScopedStoreOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StoreOptions → [FileSystemStoreOptions](./proffer.storage.filesystem.configuration.filesystemstoreoptions) → [FileSystemScopedStoreOptions](./proffer.storage.filesystem.configuration.filesystemscopedstoreoptions)<br>
Implements IStoreOptions, INamedElementOptions, IScopedStoreOptions

## Properties

### **FolderNameFormat**

Gets the folder name format.

```csharp
public string FolderNameFormat { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RootPath**

Gets or sets the root path.

```csharp
public string RootPath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AbsolutePath**

Gets the absolute path.

```csharp
public string AbsolutePath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**



```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderName**



```csharp
public string ProviderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderType**



```csharp
public string ProviderType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AccessLevel**



```csharp
public AccessLevel AccessLevel { get; set; }
```

#### Property Value

AccessLevel<br>

### **FolderName**



```csharp
public string FolderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **FileSystemScopedStoreOptions()**



```csharp
public FileSystemScopedStoreOptions()
```

---

[`< Back`](./)
