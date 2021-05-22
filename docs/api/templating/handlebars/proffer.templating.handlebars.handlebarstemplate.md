# HandlebarsTemplate

Namespace: Proffer.Templating.Handlebars

A template reference can be executed on a specific context using N:HandlebarsDotNet.

```csharp
public class HandlebarsTemplate : Proffer.Templating.ITemplate
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [HandlebarsTemplate](./proffer.templating.handlebars.handlebarstemplate.md)<br>
Implements ITemplate

## Constructors

### **HandlebarsTemplate(String)**

Initializes a new instance of the [HandlebarsTemplate](./proffer.templating.handlebars.handlebarstemplate.md) class.

```csharp
public HandlebarsTemplate(string templateContent)
```

#### Parameters

`templateContent` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Content of the template.

### **HandlebarsTemplate(IHandlebars, String)**

Initializes a new instance of the [HandlebarsTemplate](./proffer.templating.handlebars.handlebarstemplate.md) class.

```csharp
public HandlebarsTemplate(IHandlebars handlebars, string templateContent)
```

#### Parameters

`handlebars` IHandlebars<br>
The Handlebars service.

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
