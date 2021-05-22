# PublicUrlProvider

Namespace: Proffer.Storage.FileSystem.Server.Internal

Provides a way to serve files from an HTTP URL on a File System using an ASP.NET middleware.

```csharp
public class PublicUrlProvider : Proffer.Storage.FileSystem.IPublicUrlProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PublicUrlProvider](./proffer.storage.filesystem.server.internal.publicurlprovider)<br>
Implements IPublicUrlProvider

## Constructors

### **PublicUrlProvider(IOptions&lt;FileSystemStorageServerOptions&gt;)**

Initializes a new instance of the [PublicUrlProvider](./proffer.storage.filesystem.server.internal.publicurlprovider) class.

```csharp
public PublicUrlProvider(IOptions<FileSystemStorageServerOptions> options)
```

#### Parameters

`options` IOptions&lt;FileSystemStorageServerOptions&gt;<br>
The options.

## Methods

### **GetPublicUrl(String, FileSystemFileReference)**

Gets the public URL of a file reference.

```csharp
public string GetPublicUrl(string storeName, FileSystemFileReference file)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`file` FileSystemFileReference<br>
The reference holding the file path.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

            The public URL.
