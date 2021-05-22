# FileSystemStorageServiceCollectionExtensions

Namespace: Proffer.Storage

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class FileSystemStorageServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemStorageServiceCollectionExtensions](./proffer.storage.filesystemstorageservicecollectionextensions.md)

## Methods

### **AddFileSystemStorage(IServiceCollection, String)**

Registers the Proffer.Storage services to the File System on the given root path.

```csharp
public static IServiceCollection AddFileSystemStorage(IServiceCollection services, string rootPath)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`rootPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The root path.

#### Returns

IServiceCollection<br>
The service collection.

### **AddFileSystemStorage(IServiceCollection)**

Registers the Proffer.Storage services to the File System on the root path System.IO.Directory.GetCurrentDirectory.

```csharp
public static IServiceCollection AddFileSystemStorage(IServiceCollection services)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

#### Returns

IServiceCollection<br>
The service collection.
