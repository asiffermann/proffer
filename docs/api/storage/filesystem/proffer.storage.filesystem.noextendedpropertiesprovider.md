# NoExtendedPropertiesProvider

Namespace: Proffer.Storage.FileSystem

Default [IExtendedPropertiesProvider](./proffer.storage.filesystem.iextendedpropertiesprovider.md) without property storage capacity.

```csharp
public class NoExtendedPropertiesProvider : IExtendedPropertiesProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NoExtendedPropertiesProvider](./proffer.storage.filesystem.noextendedpropertiesprovider.md)<br>
Implements [IExtendedPropertiesProvider](./proffer.storage.filesystem.iextendedpropertiesprovider.md)

## Constructors

### **NoExtendedPropertiesProvider()**



```csharp
public NoExtendedPropertiesProvider()
```

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

`extendedProperties` [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties.md)<br>
The extended properties.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task that represents the asynchronous operation.
