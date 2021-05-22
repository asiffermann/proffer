# StorageServiceCollectionExtensions

Namespace: Proffer.Storage

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class StorageServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StorageServiceCollectionExtensions](./proffer.storage.storageservicecollectionextensions.md)

## Methods

### **AddStorage(IServiceCollection)**

Registers Proffer.Storage services.

```csharp
public static IServiceCollection AddStorage(IServiceCollection services)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

#### Returns

IServiceCollection<br>
The service collection.

### **AddStorage(IServiceCollection, IConfigurationSection)**

Registers Proffer.Storage services and configures it with the given section.

```csharp
public static IServiceCollection AddStorage(IServiceCollection services, IConfigurationSection configurationSection)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`configurationSection` IConfigurationSection<br>
The configuration section.

#### Returns

IServiceCollection<br>
The service collection.

### **AddStorage(IServiceCollection, IConfigurationRoot, String)**

Registers Proffer.Storage services and configures it from the given  at section .

```csharp
public static IServiceCollection AddStorage(IServiceCollection services, IConfigurationRoot configurationRoot, string sectionName)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`configurationRoot` IConfigurationRoot<br>
The configuration root.

`sectionName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the section.

#### Returns

IServiceCollection<br>

            The service collection.
