# EmailOptions

Namespace: Proffer.Email

The Proffer.Email options with providers.

```csharp
public class EmailOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EmailOptions](./proffer.email.emailoptions)

## Fields

### **DefaultConfigurationSectionName**

The default configuration section name.

```csharp
public static string DefaultConfigurationSectionName;
```

## Properties

### **Provider**

Gets or sets the provider options.

```csharp
public EmailProviderOptions Provider { get; set; }
```

#### Property Value

[EmailProviderOptions](./proffer.email.emailprovideroptions)<br>

### **DefaultSender**

Gets or sets the default sender email address.

```csharp
public EmailAddress DefaultSender { get; set; }
```

#### Property Value

[EmailAddress](./proffer.email.internal.emailaddress)<br>

### **TemplateStorage**

Gets or sets the template storage key to load templates from.

```csharp
public string TemplateStorage { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Mockup**

Gets or sets the mockup options.

```csharp
public MockupOptions Mockup { get; set; }
```

#### Property Value

[MockupOptions](./proffer.email.mockupoptions)<br>

## Constructors

### **EmailOptions()**



```csharp
public EmailOptions()
```
