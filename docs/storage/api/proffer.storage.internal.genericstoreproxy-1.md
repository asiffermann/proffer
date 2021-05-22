# GenericStoreProxy&lt;TOptions&gt;

Namespace: Proffer.Storage.Internal

Generic [IStore](./proffer.storage.istore) proxy to allow direct dependency injection of a [IStore&lt;TOptions&gt;](./proffer.storage.istore-1).

```csharp
public class GenericStoreProxy<TOptions> : Proffer.Storage.IStore, 
```

#### Type Parameters

`TOptions`<br>
The type of the store options.

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [GenericStoreProxy&lt;TOptions&gt;](./proffer.storage.internal.genericstoreproxy-1)<br>
Implements [IStore](./proffer.storage.istore), IStore&lt;TOptions&gt;

## Properties

### **Name**

Gets the name of the store.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **GenericStoreProxy(IStorageFactory, IOptions&lt;TOptions&gt;)**

Initializes a new instance of the [GenericStoreProxy&lt;TOptions&gt;](./proffer.storage.internal.genericstoreproxy-1) class.

```csharp
public GenericStoreProxy(IStorageFactory factory, IOptions<TOptions> options)
```

#### Parameters

`factory` [IStorageFactory](./proffer.storage.istoragefactory)<br>
The storage factory.

`options` IOptions&lt;TOptions&gt;<br>
The options.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>

## Methods

### **InitAsync(CancellationToken)**

Initializes the store by creating a container in its [IStorageProvider](./proffer.storage.istorageprovider).

```csharp
public Task InitAsync(CancellationToken cancellationToken)
```

#### Parameters

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **DeleteAsync(IPrivateFileReference)**

Deletes the file.

```csharp
public Task DeleteAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **GetAsync(Uri, Boolean)**

Gets the file reference from URI.

```csharp
public ValueTask<IFileReference> GetAsync(Uri file, bool withMetadata)
```

#### Parameters

`file` Uri<br>
The file uniform resource identifier (URI).

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for the file.

#### Returns

ValueTask&lt;IFileReference&gt;<br>

            The  at path.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>

### **GetAsync(IPrivateFileReference, Boolean)**

Gets the file reference from path.

```csharp
public ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for the file.

#### Returns

ValueTask&lt;IFileReference&gt;<br>

            The  at path.

### **ListAsync(String, Boolean, Boolean)**

Lists the files under .

```csharp
public ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata)
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
public ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata)
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

### **ReadAllBytesAsync(IPrivateFileReference)**

Reads the file content.

```csharp
public ValueTask<Byte[]> ReadAllBytesAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;Byte[]&gt;<br>

            A  containing the file content.

### **ReadAllTextAsync(IPrivateFileReference)**

Reads the file content.

```csharp
public ValueTask<string> ReadAllTextAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;String&gt;<br>

            A  containing the file content.

### **ReadAsync(IPrivateFileReference)**

Reads the file content.

```csharp
public ValueTask<Stream> ReadAsync(IPrivateFileReference file)
```

#### Parameters

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

#### Returns

ValueTask&lt;Stream&gt;<br>

            A  containing the file content.

### **SaveAsync(Stream, IPrivateFileReference, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
public ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`data` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The file content.

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content-type of the file.

`overwritePolicy` [OverwritePolicy](./proffer.storage.overwritepolicy)<br>
The overwrite policy.

`metadata` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The metadata.

#### Returns

ValueTask&lt;IFileReference&gt;<br>

            The saved .

#### Exceptions

[FileAlreadyExistsException](./proffer.storage.exceptions.filealreadyexistsexception)<br>

### **SaveAsync(Byte[], IPrivateFileReference, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
public ValueTask<IFileReference> SaveAsync(Byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The file content.

`file` [IPrivateFileReference](./proffer.storage.iprivatefilereference)<br>
The reference holding the file path.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content-type of the file.

`overwritePolicy` [OverwritePolicy](./proffer.storage.overwritepolicy)<br>
The overwrite policy.

`metadata` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
The metadata.

#### Returns

ValueTask&lt;IFileReference&gt;<br>

            The saved .

#### Exceptions

[FileAlreadyExistsException](./proffer.storage.exceptions.filealreadyexistsexception)<br>

### **GetSharedAccessSignatureAsync(ISharedAccessPolicy)**

Gets a shared access signature.

```csharp
public ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy)
```

#### Parameters

`policy` [ISharedAccessPolicy](./proffer.storage.isharedaccesspolicy)<br>
The policy.

#### Returns

ValueTask&lt;String&gt;<br>

            A shared access signature to read or list the store files.

#### Exceptions

[NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception)<br>
