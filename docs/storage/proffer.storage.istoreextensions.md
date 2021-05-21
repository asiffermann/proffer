# IStoreExtensions

Namespace: Proffer.Storage

[IStore](./proffer.storage.istore) extension methods.

```csharp
public static class IStoreExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IStoreExtensions](./proffer.storage.istoreextensions)

## Methods

### **ListAsync(IStore, String, Boolean, Boolean)**

Lists the files under .

```csharp
public static ValueTask<IFileReference[]> ListAsync(IStore store, string path, bool recursive, bool withMetadata)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path.

`recursive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, recurse the listing across folders.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for each file.

#### Returns

ValueTask&lt;IFileReference[]&gt;<br>

            The  list under .

### **ListAsync(IStore, String, String, Boolean, Boolean)**

Lists the files under  matching the .

```csharp
public static ValueTask<IFileReference[]> ListAsync(IStore store, string path, string searchPattern, bool recursive, bool withMetadata)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

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

### **DeleteAsync(IStore, String)**

Deletes the file.

```csharp
public static Task DeleteAsync(IStore store, string path)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **GetAsync(IStore, String, Boolean)**

Gets the file reference from path.

```csharp
public static ValueTask<IFileReference> GetAsync(IStore store, string path, bool withMetadata)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

`withMetadata` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, fetch metadata for the file.

#### Returns

ValueTask&lt;IFileReference&gt;<br>

            The  at path.

### **ReadAsync(IStore, String)**

Reads the file content.

```csharp
public static ValueTask<Stream> ReadAsync(IStore store, string path)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

#### Returns

ValueTask&lt;Stream&gt;<br>

            A  containing the file content.

### **ReadAllBytesAsync(IStore, String)**

Reads the file content.

```csharp
public static ValueTask<Byte[]> ReadAllBytesAsync(IStore store, string path)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

#### Returns

ValueTask&lt;Byte[]&gt;<br>

            A  containing the file content.

### **ReadAllTextAsync(IStore, String)**

Reads the file content.

```csharp
public static ValueTask<string> ReadAllTextAsync(IStore store, string path)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

#### Returns

ValueTask&lt;String&gt;<br>

            A  containing the file content.

### **SaveAsync(IStore, Byte[], String, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
public static ValueTask<IFileReference> SaveAsync(IStore store, Byte[] data, string path, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The file content.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

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

### **SaveAsync(IStore, Stream, String, String, OverwritePolicy, IDictionary&lt;String, String&gt;)**

Saves the file.

```csharp
public static ValueTask<IFileReference> SaveAsync(IStore store, Stream data, string path, string contentType, OverwritePolicy overwritePolicy, IDictionary<string, string> metadata)
```

#### Parameters

`store` [IStore](./proffer.storage.istore)<br>
The store.

`data` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The file content.

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

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
