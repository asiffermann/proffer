# SendGridEmailProviderType

Namespace: Proffer.Email.SendGrid

Builds [SendGridEmailProvider](./proffer.email.sendgrid.sendgridemailprovider.md).

```csharp
public class SendGridEmailProviderType : Proffer.Email.IEmailProviderType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SendGridEmailProviderType](./proffer.email.sendgrid.sendgridemailprovidertype.md)<br>
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

### **SendGridEmailProviderType()**



```csharp
public SendGridEmailProviderType()
```

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
