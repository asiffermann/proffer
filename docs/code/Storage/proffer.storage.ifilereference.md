[`< Back`](./)

---

# IFileReference

Namespace: Proffer.Storage

A reference of a stored file at a given path.

```csharp
public interface IFileReference : IPrivateFileReference
```

Implements [IPrivateFileReference](./proffer.storage.iprivatefilereference)

## Properties

### **PublicUrl**

Gets the public URL.

```csharp
public abstract string PublicUrl { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Properties**

Gets the properties.

```csharp
public abstract IFileProperties Properties { get; }
```

#### Property Value

[IFileProperties](./proffer.storage.ifileproperties)<br>

## Methods

### **ReadToStreamAsync(Stream)**

Reads the file content into the given stream.

```csharp
Task ReadToStreamAsync(Stream targetStream)
```

#### Parameters

`targetStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The target stream.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

### **ReadAsync()**

Reads the file content.

```csharp
ValueTask<Stream> ReadAsync()
```

#### Returns

ValueTask&lt;Stream&gt;<br>
A  containing the file content.

### **ReadAllTextAsync()**

Reads the file content.

```csharp
ValueTask<string> ReadAllTextAsync()
```

#### Returns

ValueTask&lt;String&gt;<br>
A  containing the file content.

### **ReadAllBytesAsync()**

Reads the file content.

```csharp
ValueTask<Byte[]> ReadAllBytesAsync()
```

#### Returns

ValueTask&lt;Byte[]&gt;<br>
A  containing the file content.

### **DeleteAsync()**

Deletes the file.

```csharp
Task DeleteAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

### **UpdateAsync(Stream)**

Updates the file content with the given [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream).

```csharp
Task UpdateAsync(Stream stream)
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
Task SavePropertiesAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

### **GetSharedAccessSignature(ISharedAccessPolicy)**

Gets a shared access signature.

```csharp
ValueTask<string> GetSharedAccessSignature(ISharedAccessPolicy policy)
```

#### Parameters

`policy` [ISharedAccessPolicy](./proffer.storage.isharedaccesspolicy)<br>
The policy.

#### Returns

ValueTask&lt;String&gt;<br>
A shared access signature to read file.

### **FetchProperties()**

Fetches the file properties.

```csharp
Task FetchProperties()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.

---

[`< Back`](./)
