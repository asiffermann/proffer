# EmailServiceCollectionExtensions

Namespace: Proffer.Email

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class EmailServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EmailServiceCollectionExtensions](./proffer.email.emailservicecollectionextensions.md)

## Methods

### **AddEmail(IServiceCollection, IConfigurationRoot, String)**

Registers Proffer.Email services and configures it from the given  at section .

```csharp
public static IServiceCollection AddEmail(IServiceCollection services, IConfigurationRoot configurationRoot, string sectionName)
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

### **AddEmail(IServiceCollection, IConfigurationSection)**

Registers Proffer.Email services and configures it with the given section.

```csharp
public static IServiceCollection AddEmail(IServiceCollection services, IConfigurationSection configurationSection)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

`configurationSection` IConfigurationSection<br>
The configuration section.

#### Returns

IServiceCollection<br>
The service collection.
