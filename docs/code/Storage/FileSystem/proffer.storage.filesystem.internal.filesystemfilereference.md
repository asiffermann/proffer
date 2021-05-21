[`< Back`](./)

---

# FileSystemFileReference

Namespace: Proffer.Storage.FileSystem.Internal

A reference of a stored file at a given path on a File System.

```csharp
public class FileSystemFileReference : Proffer.Storage.IFileReference, Proffer.Storage.IPrivateFileReference
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemFileReference](./proffer.storage.filesystem.internal.filesystemfilereference)<br>
Implements IFileReference, IPrivateFileReference

## Properties

### **FileSystemPath**

Gets the file system path.

```csharp
public string FileSystemPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Path**

Gets the file path.

```csharp
public string Path { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PublicUrl**

Gets the public URL.

```csharp
public string PublicUrl { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Properties**

Gets the properties.

```csharp
public IFileProperties Properties { get; }
```

#### Property Value

IFileProperties<br>

## Constructors

### **FileSystemFileReference(String, String, FileSystemStore, Boolean, FileExtendedProperties, IPublicUrlProvider, IExtendedPropertiesProvider)**

Initializes a new instance of the [FileSystemFileReference](./proffer.storage.filesystem.internal.filesystemfilereference) class.

```csharp
public FileSystemFileReference(string filePath, string path, FileSystemStore store, bool withMetadata, FileExtendedProperties extendedProperties, IPublicUrlProvider publicUrlProvider, IExtendedPropertiesProvider extendedPropertiesProvider)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file system path.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path.

`store` [FileSystemStore](./proffer.storage.filesystem.filesystemstore)<br>
The store.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, the metadata for the file have been fetched.

`extendedProperties` [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties)<br>
The extended properties.

`publicUrlProvider` [IPublicUrlProvider](./proffer.storage.filesystem.ipublicurlprovider)<br>
The public URL provider.

`extendedPropertiesProvider` [IExtendedPropertiesProvider](./proffer.storage.filesystem.iextendedpropertiesprovider)<br>
The extended properties provider.

## Methods

### **DeleteAsync()**

Deletes the file.

```csharp
public Task DeleteAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **ReadAllBytesAsync()**

Reads the file content.

```csharp
public ValueTask<Byte[]> ReadAllBytesAsync()
```

#### Returns

[ValueTask&lt;Byte[]&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)<br>

            A  containing the file content.

### **ReadAllTextAsync()**

Reads the file content.

```csharp
public ValueTask<string> ReadAllTextAsync()
```

#### Returns

[ValueTask&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)<br>

            A  containing the file content.

### **ReadAsync()**

Reads the file content.

```csharp
public ValueTask<Stream> ReadAsync()
```

#### Returns

[ValueTask&lt;Stream&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)<br>

            A  containing the file content.

### **ReadToStreamAsync(Stream)**

Reads the file content into the given stream.

```csharp
public Task ReadToStreamAsync(Stream targetStream)
```

#### Parameters

`targetStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The target stream.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **UpdateAsync(Stream)**

Updates the file content with the given [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream).

```csharp
public Task UpdateAsync(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The new file content.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SavePropertiesAsync()**

Saves the file properties.

```csharp
public Task SavePropertiesAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **GetSharedAccessSignature(ISharedAccessPolicy)**

Gets a shared access signature.

```csharp
public ValueTask<string> GetSharedAccessSignature(ISharedAccessPolicy policy)
```

#### Parameters

`policy` ISharedAccessPolicy<br>
The policy.

#### Returns

[ValueTask&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)<br>
A shared access signature to read file.

### **FetchProperties()**

Fetches the file properties.

```csharp
public Task FetchProperties()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

---

[`< Back`](./)
