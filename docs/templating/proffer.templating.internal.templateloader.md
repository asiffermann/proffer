# TemplateLoader

Namespace: Proffer.Templating.Internal

Loads template references from an Proffer.Storage.IStore and cache results.

```csharp
public class TemplateLoader : Proffer.Templating.ITemplateLoader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TemplateLoader](./proffer.templating.internal.templateloader)<br>
Implements [ITemplateLoader](./proffer.templating.itemplateloader)

## Constructors

### **TemplateLoader(IStore, IEnumerable&lt;ITemplateProvider&gt;, IMemoryCache)**

Initializes a new instance of the [TemplateLoader](./proffer.templating.internal.templateloader) class.

```csharp
public TemplateLoader(IStore store, IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache)
```

#### Parameters

`store` IStore<br>
The store.

`providers` [IEnumerable&lt;ITemplateProvider&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The providers.

`memoryCache` IMemoryCache<br>
The memory cache.

## Methods

### **GetTemplate(String)**

Gets a template by name.

```csharp
public Task<ITemplate> GetTemplate(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template name.

#### Returns

[Task&lt;ITemplate&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

            The matching .
