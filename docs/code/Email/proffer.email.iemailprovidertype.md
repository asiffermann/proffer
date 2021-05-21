[`< Back`](./)

---

# IEmailProviderType

Namespace: Proffer.Email

Builds providers using a particular messaging protocol or API.

```csharp
public interface IEmailProviderType
```

## Properties

### **Name**

Gets the name.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **BuildProvider(IEmailProviderOptions)**

Builds the provider.

```csharp
IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
```

#### Parameters

`providerOptions` [IEmailProviderOptions](./proffer.email.iemailprovideroptions)<br>
The provider options.

#### Returns

[IEmailProvider](./proffer.email.iemailprovider)<br>
A new .

---

[`< Back`](./)
