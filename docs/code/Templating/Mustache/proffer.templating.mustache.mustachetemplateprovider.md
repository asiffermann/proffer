[`< Back`](./)

---

# MustacheTemplateProvider

Namespace: Proffer.Templating.Mustache

A provider handles and compiles templates using N:Mustache.

```csharp
public class MustacheTemplateProvider : Proffer.Templating.ITemplateProvider, Proffer.Templating.ITemplateProviderScope
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MustacheTemplateProvider](./proffer.templating.mustache.mustachetemplateprovider)<br>
Implements ITemplateProvider, ITemplateProviderScope

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

### **MustacheTemplateProvider()**

Initializes a new instance of the [MustacheTemplateProvider](./proffer.templating.mustache.mustachetemplateprovider) class.

```csharp
public MustacheTemplateProvider()
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

            A new  compiled from the content.

### **CreateScope()**

Creates a template provider scope.

```csharp
public ITemplateProviderScope CreateScope()
```

#### Returns

ITemplateProviderScope<br>

            A new templating provider scope.

### **RegisterPartial(String, String)**

Registers a partial template with a name.

```csharp
public void RegisterPartial(string name, string template)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name.

`template` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template.

#### Exceptions

[NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception)<br>

---

[`< Back`](./)
