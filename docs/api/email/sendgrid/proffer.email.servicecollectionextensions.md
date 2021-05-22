# ServiceCollectionExtensions

Namespace: Proffer.Email

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class ServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ServiceCollectionExtensions](./proffer.email.servicecollectionextensions.md)

## Methods

### **AddSendGridEmail(IServiceCollection)**

Registers the Proffer.Email services to the N:SendGrid API.

```csharp
public static IServiceCollection AddSendGridEmail(IServiceCollection services)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

#### Returns

IServiceCollection<br>
The service collection.
