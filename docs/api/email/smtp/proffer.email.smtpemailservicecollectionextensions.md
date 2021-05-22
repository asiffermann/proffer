# SmtpEmailServiceCollectionExtensions

Namespace: Proffer.Email

Microsoft.Extensions.DependencyInjection.IServiceCollection extension methods.

```csharp
public static class SmtpEmailServiceCollectionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SmtpEmailServiceCollectionExtensions](./proffer.email.smtpemailservicecollectionextensions.md)

## Methods

### **AddSmtpEmail(IServiceCollection)**

Registers the Proffer.Email services to a SMTP server with N:MailKit.

```csharp
public static IServiceCollection AddSmtpEmail(IServiceCollection services)
```

#### Parameters

`services` IServiceCollection<br>
The service collection.

#### Returns

IServiceCollection<br>
The service collection.
