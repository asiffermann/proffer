# SmtpEmailProviderType

Namespace: Proffer.Email.Smtp

Builds [SmtpEmailProvider](./proffer.email.smtp.smtpemailprovider).

```csharp
public class SmtpEmailProviderType : Proffer.Email.IEmailProviderType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SmtpEmailProviderType](./proffer.email.smtp.smtpemailprovidertype)<br>
Implements IEmailProviderType

## Properties

### **Name**

Gets the name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **SmtpEmailProviderType(IServiceProvider)**

Initializes a new instance of the [SmtpEmailProviderType](./proffer.email.smtp.smtpemailprovidertype) class.

```csharp
public SmtpEmailProviderType(IServiceProvider serviceProvider)
```

#### Parameters

`serviceProvider` IServiceProvider<br>
The service provider.

## Methods

### **BuildProvider(IEmailProviderOptions)**

Builds the provider.

```csharp
public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
```

#### Parameters

`providerOptions` IEmailProviderOptions<br>
The provider options.

#### Returns

IEmailProvider<br>

            A new .
