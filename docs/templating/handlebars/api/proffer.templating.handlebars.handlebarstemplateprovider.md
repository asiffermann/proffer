# HandlebarsTemplateProvider

Namespace: Proffer.Templating.Handlebars

A provider handles and compiles templates using N:HandlebarsDotNet.

```csharp
public class HandlebarsTemplateProvider : Proffer.Templating.ITemplateProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [HandlebarsTemplateProvider](./proffer.templating.handlebars.handlebarstemplateprovider)<br>
Implements ITemplateProvider

## Properties

### **MimeTypes**

Gets the MIME types.

```csharp
public ISet<string> MimeTypes { get; }
```

#### Property Value

[ISet&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1)<br>

### **Extensions**

Gets the extensions.

```csharp
public ISet<string> Extensions { get; }
```

#### Property Value

[ISet&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1)<br>

## Constructors

### **HandlebarsTemplateProvider()**

Initializes a new instance of the [HandlebarsTemplateProvider](./proffer.templating.handlebars.handlebarstemplateprovider) class.

```csharp
public HandlebarsTemplateProvider()
```

## Methods

### **Compile(String)**

Compiles the specified template content.

```csharp
public ITemplate Compile(string templateContent)
```

#### Parameters

`templateContent` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Content of the template.

#### Returns

ITemplate<br>
A compiled .

### **CreateScope()**

Creates a template provider scope.

```csharp
public ITemplateProviderScope CreateScope()
```

#### Returns

ITemplateProviderScope<br>

            A new templating provider scope.
