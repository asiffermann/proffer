# MustacheTemplate

Namespace: Proffer.Templating.Mustache

A template reference can be executed on a specific context using N:Mustache.

```csharp
public class MustacheTemplate : Proffer.Templating.ITemplate
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MustacheTemplate](./proffer.templating.mustache.mustachetemplate)<br>
Implements ITemplate

## Constructors

### **MustacheTemplate(String)**

Initializes a new instance of the [MustacheTemplate](./proffer.templating.mustache.mustachetemplate) class.

```csharp
public MustacheTemplate(string templateContent)
```

#### Parameters

`templateContent` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Content of the template.

## Methods

### **Apply(Object)**

Applies the specified context on the template.

```csharp
public string Apply(object context)
```

#### Parameters

`context` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The context.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

            The templated result.

#### Exceptions

Proffer.Templating.InvalidContextException<br>

### **Apply(Object, IFormatProvider)**

Applies the specified context on the template with format provider.

```csharp
public string Apply(object context, IFormatProvider formatProvider)
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

Proffer.Templating.InvalidContextException<br>
