# FileSystemPropertiesJsonServiceCollectionExtensions

Namespace: Proffer.Storage

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class FileSystemPropertiesJsonServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemPropertiesJsonServiceCollectionExtensions](./proffer.storage.filesystempropertiesjsonservicecollectionextensions.md)

## Methods

### **AddFileSystemExtendedProperties(IServiceCollection, Action&lt;FileSystemPropertiesJsonOptions&gt;)**

Registers a File System extended properties provider that stores it in JSON files.

```csharp
public static IServiceCollection AddFileSystemExtendedProperties(IServiceCollection services, Action<FileSystemPropertiesJsonOptions> configure)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`configure` [Action&lt;FileSystemPropertiesJsonOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
The action to configure options.

#### Returns

IServiceCollection<br>

            The service collection.
