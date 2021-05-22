# ProviderNotFoundException

Namespace: Proffer.Templating.Exceptions

Thrown when a matching provider was not found for a given template.

```csharp
public class ProviderNotFoundException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [ProviderNotFoundException](./proffer.templating.exceptions.providernotfoundexception.md)<br>
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### **TargetSite**



```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **StackTrace**



```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Message**



```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**



```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**



```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**



```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**



```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**



```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **ProviderNotFoundException(String, String, String)**

Initializes a new instance of the [ProviderNotFoundException](./proffer.templating.exceptions.providernotfoundexception.md) class.

```csharp
public ProviderNotFoundException(string templateName, string extension, string contentType)
```

#### Parameters

`templateName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the unmatched template.

`extension` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The extension of the unmatched template.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The MIME type of the unmatched template.
