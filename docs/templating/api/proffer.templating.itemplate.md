# ITemplate

Namespace: Proffer.Templating

A template reference can be executed on a specific context using an Apply method.

```csharp
public interface ITemplate
```

## Methods

### **Apply(Object)**

Applies the specified context on the template.

```csharp
string Apply(object context)
```

#### Parameters

`context` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The context.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The templated result.

#### Exceptions

[InvalidContextException](./proffer.templating.invalidcontextexception)<br>

### **Apply(Object, IFormatProvider)**

Applies the specified context on the template with format provider.

```csharp
string Apply(object context, IFormatProvider formatProvider)
```

#### Parameters

`context` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The context.

`formatProvider` [IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/system.iformatprovider)<br>
The format provider.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

            The templated result.

#### Exceptions

[InvalidContextException](./proffer.templating.invalidcontextexception)<br>
