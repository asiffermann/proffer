[`< Back`](./)

---

# FileSystemStoreOptions

Namespace: Proffer.Storage.FileSystem.Configuration

Options for an [FileSystemStore](./proffer.storage.filesystem.filesystemstore).

```csharp
public class FileSystemStoreOptions : Proffer.Storage.Configuration.StoreOptions, Proffer.Storage.Configuration.IStoreOptions, Proffer.Configuration.INamedElementOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StoreOptions → [FileSystemStoreOptions](./proffer.storage.filesystem.configuration.filesystemstoreoptions)<br>
Implements IStoreOptions, INamedElementOptions

## Properties

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

### **FileSystemStoreOptions()**



```csharp
public FileSystemStoreOptions()
```

## Methods

### **Validate(Boolean)**

Validates the options.

```csharp
public IEnumerable<IOptionError> Validate(bool throwOnError)
```

#### Parameters

`throwOnError` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, throws an exception when the validation fails with any .

#### Returns

[IEnumerable&lt;IOptionError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

            The  returned by the validation, if any.

#### Exceptions

Proffer.Storage.Exceptions.BadStoreConfiguration<br>

---

[`< Back`](./)
