# IPublicUrlProvider

Namespace: Proffer.Storage.FileSystem

Provides a way to serve files from an HTTP URL on a File System.

```csharp
public interface IPublicUrlProvider
```

## Methods

### **GetPublicUrl(String, FileSystemFileReference)**

Gets the public URL of a file reference.

```csharp
string GetPublicUrl(string storeName, FileSystemFileReference file)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`file` [FileSystemFileReference](./proffer.storage.filesystem.internal.filesystemfilereference.md)<br>
The reference holding the file path.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The public URL.
