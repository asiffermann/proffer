# ExtendedPropertiesProvider

Namespace: Proffer.Storage.FileSystem.Properties.Json.Internal

Provides a way to store and retrieve extended file properties to match the requirements of Proffer.Storage.IFileProperties on a File System using JSON files.

```csharp
public class ExtendedPropertiesProvider : Proffer.Storage.FileSystem.IExtendedPropertiesProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ExtendedPropertiesProvider](./proffer.storage.filesystem.properties.json.internal.extendedpropertiesprovider.md)<br>
Implements IExtendedPropertiesProvider

## Constructors

### **ExtendedPropertiesProvider(IOptions&lt;FileSystemPropertiesJsonOptions&gt;)**

Initializes a new instance of the [ExtendedPropertiesProvider](./proffer.storage.filesystem.properties.json.internal.extendedpropertiesprovider.md) class.

```csharp
public ExtendedPropertiesProvider(IOptions<FileSystemPropertiesJsonOptions> options)
```

#### Parameters

`options` IOptions&lt;FileSystemPropertiesJsonOptions&gt;<br>
The options.

## Methods

### **GetExtendedPropertiesAsync(String, IPrivateFileReference)**

Gets the extended properties of a file reference.

```csharp
public ValueTask<FileExtendedProperties> GetExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file)
```

#### Parameters

`storeAbsolutePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The store absolute path.

`file` IPrivateFileReference<br>
The reference holding the file path.

#### Returns

ValueTask&lt;FileExtendedProperties&gt;<br>

            A loaded  instance or a default one if not found.

### **SaveExtendedPropertiesAsync(String, IPrivateFileReference, FileExtendedProperties)**

Saves the extended properties for a file reference.

```csharp
public Task SaveExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file, FileExtendedProperties extendedProperties)
```

#### Parameters

`storeAbsolutePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The store absolute path.

`file` IPrivateFileReference<br>
The reference holding the file path.

`extendedProperties` FileExtendedProperties<br>
The extended properties.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.
