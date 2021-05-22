# IStore

Namespace: Proffer.Storage

A store allows to save, list or read files on a container in its configured [IStorageProvider](./proffer.storage.istorageprovider.md).

```csharp
public interface IStore
```

## Properties

### **Name**

Gets the name of the store.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **InitAsync(CancellationToken)**

Initializes the store by creating a container in its [IStorageProvider](./proffer.storage.istorageprovider.md).

```csharp
Task InitAsync(CancellationToken cancellationToken)
```

#### Parameters

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

### **ListAsync(String, Boolean, Boolean)**

Lists the files under .

```csharp
ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path.

`recursive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, recurse the listing across folders.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for each file.

#### Returns

ValueTask&lt;IFileReference[]&gt;<br>
The  list under .

### **ListAsync(String, String, Boolean, Boolean)**

Lists the files under  matching the .

```csharp
ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path.

`searchPattern` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The search pattern.

`recursive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, recurse the listing across folders.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for each file.

#### Returns

ValueTask&lt;IFileReference[]&gt;<br>
The  list under  matching the .

### **GetAsync(IPrivateFileReference, Boolean)**

Gets the file reference from path.

```csharp
ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for the file.

#### Returns

ValueTask&lt;IFileReference&gt;<br>
The  at path.

### **GetAsync(Uri, Boolean)**

Gets the file reference from URI.

```csharp
ValueTask<IFileReference> GetAsync(Uri uri, bool withMetadata)
```

#### Parameters

`uri` Uri<br>
The file uniform resource identifier (URI).

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for the file.

#### Returns

ValueTask&lt;IFileReference&gt;<br>
The  at path.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>

### **DeleteAsync(IPrivateFileReference)**

Deletes the file.

```csharp
Task DeleteAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

### **ReadAsync(IPrivateFileReference)**

Reads the file content.

```csharp
ValueTask<Stream> ReadAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;Stream&gt;<br>
A  containing the file content.

### **ReadAllBytesAsync(IPrivateFileReference)**

Reads the file content.

```csharp
ValueTask<Byte[]> ReadAllBytesAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;Byte[]&gt;<br>
A  containing the file content.

### **ReadAllTextAsync(IPrivateFileReference)**

Reads the file content.

```csharp
ValueTask<string> ReadAllTextAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;String&gt;<br>
A  containing the file content.

### **SaveAsync(Byte[], IPrivateFileReference, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
ValueTask<IFileReference> SaveAsync(Byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The file content.

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content-type of the file.

`overwritePolicy` [OverwritePolicy](./proffer.storage.overwritepolicy.md)<br>
The overwrite policy.

`metadata` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The metadata.

#### Returns

ValueTask&lt;IFileReference&gt;<br>
The saved .

#### Exceptions

[FileAlreadyExistsException](./proffer.storage.exceptions.filealreadyexistsexception.md)<br>

### **SaveAsync(Stream, IPrivateFileReference, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`data` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The file content.

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)<br>
The reference holding the file path.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content-type of the file.

`overwritePolicy` [OverwritePolicy](./proffer.storage.overwritepolicy.md)<br>
The overwrite policy.

`metadata` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The metadata.

#### Returns

ValueTask&lt;IFileReference&gt;<br>
The saved .

#### Exceptions

[FileAlreadyExistsException](./proffer.storage.exceptions.filealreadyexistsexception.md)<br>

### **GetSharedAccessSignatureAsync(ISharedAccessPolicy)**

Gets a shared access signature.

```csharp
ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy)
```

#### Parameters

`policy` [ISharedAccessPolicy](./proffer.storage.isharedaccesspolicy.md)<br>
The policy.

#### Returns

ValueTask&lt;String&gt;<br>
A shared access signature to read or list the store files.

#### Exceptions

[NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception)<br>
