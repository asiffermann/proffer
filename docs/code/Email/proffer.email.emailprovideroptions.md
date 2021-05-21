[`< Back`](./)

---

# EmailProviderOptions

Namespace: Proffer.Email

Generic options for a [IEmailProvider](./proffer.email.iemailprovider).

```csharp
public class EmailProviderOptions : IEmailProviderOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EmailProviderOptions](./proffer.email.emailprovideroptions)<br>
Implements [IEmailProviderOptions](./proffer.email.iemailprovideroptions)

## Properties

### **Type**

Gets or sets the type of the provider.

```csharp
public string Type { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Parameters**

Gets or sets the provider parameters.

```csharp
public Dictionary<string, string> Parameters { get; set; }
```

#### Property Value

[Dictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Constructors

### **EmailProviderOptions()**



```csharp
public EmailProviderOptions()
```

---

[`< Back`](./)
