# OptionError

Namespace: Proffer.Configuration

A generic error reported from the options validation.

```csharp
public class OptionError : IOptionError
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptionError](./proffer.configuration.optionerror.md)<br>
Implements [IOptionError](./proffer.configuration.ioptionerror.md)

## Properties

### **PropertyName**

Gets the name of the faulted property.

```csharp
public string PropertyName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ErrorMessage**

Gets the error message.

```csharp
public string ErrorMessage { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **OptionError()**



```csharp
public OptionError()
```
