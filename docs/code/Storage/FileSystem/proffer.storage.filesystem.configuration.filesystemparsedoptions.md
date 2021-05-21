[`< Back`](./)

---

# FileSystemParsedOptions

Namespace: Proffer.Storage.FileSystem.Configuration

Typed File System options parsed from the dynamic configuration.

```csharp
public class FileSystemParsedOptions : Proffer.Storage.Configuration.IParsedOptions`3[[Proffer.Storage.FileSystem.Configuration.FileSystemProviderOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.FileSystem.Configuration.FileSystemStoreOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.FileSystem.Configuration.FileSystemScopedStoreOptions, Proffer.Storage.FileSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemParsedOptions](./proffer.storage.filesystem.configuration.filesystemparsedoptions)<br>
Implements IParsedOptions&lt;FileSystemProviderOptions, FileSystemStoreOptions, FileSystemScopedStoreOptions&gt;

## Properties

### **Name**

Gets the name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConnectionStrings**

Gets or sets the connection strings.

```csharp
public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedProviders**

Gets or sets the parsed provider instances options.

```csharp
public IReadOnlyDictionary<string, FileSystemProviderOptions> ParsedProviders { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, FileSystemProviderOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedStores**

Gets or sets the parsed stores options.

```csharp
public IReadOnlyDictionary<string, FileSystemStoreOptions> ParsedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, FileSystemStoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedScopedStores**

Gets or sets the parsed scoped stores options.

```csharp
public IReadOnlyDictionary<string, FileSystemScopedStoreOptions> ParsedScopedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, FileSystemScopedStoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **RootPath**

Gets or sets the root path.

```csharp
public string RootPath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **FileSystemParsedOptions()**



```csharp
public FileSystemParsedOptions()
```

## Methods

### **BindProviderOptions(FileSystemProviderOptions)**

Binds the provider instance options.

```csharp
public void BindProviderOptions(FileSystemProviderOptions providerInstanceOptions)
```

#### Parameters

`providerInstanceOptions` [FileSystemProviderOptions](./proffer.storage.filesystem.configuration.filesystemprovideroptions)<br>
The provider instance options.

### **BindStoreOptions(FileSystemStoreOptions, FileSystemProviderOptions)**

Binds the store options.

```csharp
public void BindStoreOptions(FileSystemStoreOptions storeOptions, FileSystemProviderOptions providerInstanceOptions)
```

#### Parameters

`storeOptions` [FileSystemStoreOptions](./proffer.storage.filesystem.configuration.filesystemstoreoptions)<br>
The store options.

`providerInstanceOptions` [FileSystemProviderOptions](./proffer.storage.filesystem.configuration.filesystemprovideroptions)<br>
The provider instance options.

---

[`< Back`](./)
