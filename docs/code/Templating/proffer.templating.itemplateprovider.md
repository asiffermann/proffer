[`< Back`](./)

---

# ITemplateProvider

Namespace: Proffer.Templating

A provider handles and compiles templates using a particular templating library.

```csharp
public interface ITemplateProvider
```

## Properties

### **MimeTypes**

Gets the MIME types.

```csharp
public abstract ISet<string> MimeTypes { get; }
```

#### Property Value

[ISet&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1)<br>

### **Extensions**

Gets the extensions.

```csharp
public abstract ISet<string> Extensions { get; }
```

#### Property Value

[ISet&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1)<br>

## Methods

### **CreateScope()**

Creates a template provider scope.

```csharp
ITemplateProviderScope CreateScope()
```

#### Returns

[ITemplateProviderScope](./proffer.templating.itemplateproviderscope)<br>
A new templating provider scope.

---

[`< Back`](./)
