# FileSystemStorageServerOptions

Namespace: Proffer.Storage.FileSystem.Server

Options for a [FileSystemStorageServerMiddleware](./proffer.storage.filesystem.server.filesystemstorageservermiddleware.md).

```csharp
public class FileSystemStorageServerOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemStorageServerOptions](./proffer.storage.filesystem.server.filesystemstorageserveroptions.md)

## Properties

### **BaseUri**

Gets or sets the base URI.

```csharp
public Uri BaseUri { get; set; }
```

#### Property Value

Uri<br>

### **EndpointPath**

Gets or sets the endpoint path.

```csharp
public PathString EndpointPath { get; set; }
```

#### Property Value

PathString<br>

### **SigningKey**

Gets or sets the signing key.

```csharp
public Byte[] SigningKey { get; set; }
```

#### Property Value

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

## Constructors

### **FileSystemStorageServerOptions()**



```csharp
public FileSystemStorageServerOptions()
```
