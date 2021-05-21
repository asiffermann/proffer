[`< Back`](./)

---

# FileSystemStorageServerMiddleware

Namespace: Proffer.Storage.FileSystem.Server

ASP.NET Core middleware to serve over HTTP files stored in a Storage store.

```csharp
public class FileSystemStorageServerMiddleware
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemStorageServerMiddleware](./proffer.storage.filesystem.server.filesystemstorageservermiddleware)

## Constructors

### **FileSystemStorageServerMiddleware(RequestDelegate, IOptions&lt;FileSystemStorageServerOptions&gt;, ILogger&lt;FileSystemStorageServerMiddleware&gt;, IOptions&lt;FileSystemParsedOptions&gt;)**

Initializes a new instance of the [FileSystemStorageServerMiddleware](./proffer.storage.filesystem.server.filesystemstorageservermiddleware) class.

```csharp
public FileSystemStorageServerMiddleware(RequestDelegate next, IOptions<FileSystemStorageServerOptions> serverOptions, ILogger<FileSystemStorageServerMiddleware> logger, IOptions<FileSystemParsedOptions> fileSystemParsedOptions)
```

#### Parameters

`next` RequestDelegate<br>
The next function.

`serverOptions` IOptions&lt;FileSystemStorageServerOptions&gt;<br>
The server options.

`logger` ILogger&lt;FileSystemStorageServerMiddleware&gt;<br>
The logger.

`fileSystemParsedOptions` IOptions&lt;FileSystemParsedOptions&gt;<br>
The file system parsed options.

## Methods

### **Invoke(HttpContext)**

Invokes the middleware.

```csharp
public Task Invoke(HttpContext context)
```

#### Parameters

`context` HttpContext<br>
The context.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

---

[`< Back`](./)
