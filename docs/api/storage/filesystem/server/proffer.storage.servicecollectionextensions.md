# ServiceCollectionExtensions

Namespace: Proffer.Storage

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class ServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ServiceCollectionExtensions](./proffer.storage.servicecollectionextensions.md)

## Methods

### **AddFileSystemStorageServer(IServiceCollection, Action&lt;FileSystemStorageServerOptions&gt;)**

Adds a File System provider Storage Server, serving files over HTTP.

```csharp
public static IServiceCollection AddFileSystemStorageServer(IServiceCollection services, Action<FileSystemStorageServerOptions> configure)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`configure` [Action&lt;FileSystemStorageServerOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
The action to configure options.

#### Returns

IServiceCollection<br>
The service collection.

### **UseFileSystemStorageServer(IApplicationBuilder)**

Adds a [FileSystemStorageServerMiddleware](./proffer.storage.filesystem.server.filesystemstorageservermiddleware.md) to the application's request pipeline.

```csharp
public static IApplicationBuilder UseFileSystemStorageServer(IApplicationBuilder app)
```

#### Parameters

`app` IApplicationBuilder<br>
The application builder.

#### Returns

IApplicationBuilder<br>
The application builder.
