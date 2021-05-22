# AzureBlobsFileReference

Namespace: Proffer.Storage.Azure.Blobs.Internal

A reference of a stored file at a given path on Azure Blobs.

```csharp
public class AzureBlobsFileReference : Proffer.Storage.IFileReference, Proffer.Storage.IPrivateFileReference
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AzureBlobsFileReference](./proffer.storage.azure.blobs.internal.azureblobsfilereference.md)<br>
Implements IFileReference, IPrivateFileReference

## Properties

### **Path**

Gets the file path.

```csharp
public string Path { get; }
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

### **PublicUrl**

Gets the public URL.

```csharp
public string PublicUrl { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AzureBlobsFileReference(BlobClient, AzureBlobsFileProperties)**

Initializes a new instance of the [AzureBlobsFileReference](./proffer.storage.azure.blobs.internal.azureblobsfilereference.md) class.

```csharp
public AzureBlobsFileReference(BlobClient blobClient, AzureBlobsFileProperties properties)
```

#### Parameters

`blobClient` BlobClient<br>
The Azure Blobs client.

`properties` [AzureBlobsFileProperties](./proffer.storage.azure.blobs.internal.azureblobsfileproperties.md)<br>
The properties, if fetched.

## Methods

### **DeleteAsync()**

Deletes the file.

```csharp
public Task DeleteAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **ReadAsync()**

Reads the file content.

```csharp
public ValueTask<Stream> ReadAsync()
```

#### Returns

ValueTask&lt;Stream&gt;<br>

            A  containing the file content.

### **ReadInMemoryAsync()**

Reads the file content in memory.

```csharp
public ValueTask<MemoryStream> ReadInMemoryAsync()
```

#### Returns

ValueTask&lt;MemoryStream&gt;<br>
A new  containing the file content.

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

### **ReadAllTextAsync()**

Reads the file content.

```csharp
public ValueTask<string> ReadAllTextAsync()
```

#### Returns

ValueTask&lt;String&gt;<br>

            A  containing the file content.

### **ReadAllBytesAsync()**

Reads the file content.

```csharp
public ValueTask<Byte[]> ReadAllBytesAsync()
```

#### Returns

ValueTask&lt;Byte[]&gt;<br>

            A  containing the file content.

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

ValueTask&lt;String&gt;<br>

            A shared access signature to read file.

### **FetchProperties()**

Fetches the file properties.

```csharp
public Task FetchProperties()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.
