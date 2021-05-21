# IExtendedPropertiesProvider

Namespace: Proffer.Storage.FileSystem

Provides a way to store and retrieve extended file properties to match the requirements of Proffer.Storage.IFileProperties on a File System.

```csharp
public interface IExtendedPropertiesProvider
```

## Methods

### **GetExtendedPropertiesAsync(String, IPrivateFileReference)**

Gets the extended properties of a file reference.

```csharp
ValueTask<FileExtendedProperties> GetExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file)
```

#### Parameters

`storeAbsolutePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The store absolute path.

`file` IPrivateFileReference<br>
The reference holding the file path.

#### Returns

[ValueTask&lt;FileExtendedProperties&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)<br>
A loaded  instance or a default one if not found.

### **SaveExtendedPropertiesAsync(String, IPrivateFileReference, FileExtendedProperties)**

Saves the extended properties for a file reference.

```csharp
Task SaveExtendedPropertiesAsync(string storeAbsolutePath, IPrivateFileReference file, FileExtendedProperties extendedProperties)
```

#### Parameters

`storeAbsolutePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The store absolute path.

`file` IPrivateFileReference<br>
The reference holding the file path.

`extendedProperties` [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties)<br>
The extended properties.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.
