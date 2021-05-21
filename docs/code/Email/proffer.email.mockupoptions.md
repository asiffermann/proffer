# MockupOptions

Namespace: Proffer.Email

Options to mockup the email sender (all recipients would be redirect to the mockup recipients).

```csharp
public class MockupOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MockupOptions](./proffer.email.mockupoptions)

## Properties

### **Recipients**

Gets or sets the mockup recipients.

```csharp
public List<string> Recipients { get; set; }
```

#### Property Value

[List&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Exceptions**

Gets or sets the exceptions options.

```csharp
public MockupExceptionsOptions Exceptions { get; set; }
```

#### Property Value

[MockupExceptionsOptions](./proffer.email.mockupexceptionsoptions)<br>

### **Disclaimer**

Gets or sets the disclaimer to add at the end of a mocked up email.

```csharp
public string Disclaimer { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **MockupOptions()**



```csharp
public MockupOptions()
```
