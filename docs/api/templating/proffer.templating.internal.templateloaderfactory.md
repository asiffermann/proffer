# TemplateLoaderFactory

Namespace: Proffer.Templating.Internal

Creates [ITemplateLoader](./proffer.templating.itemplateloader.md) from Proffer.Storage.IStore.

```csharp
public class TemplateLoaderFactory : Proffer.Templating.ITemplateLoaderFactory
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TemplateLoaderFactory](./proffer.templating.internal.templateloaderfactory.md)<br>
Implements [ITemplateLoaderFactory](./proffer.templating.itemplateloaderfactory.md)

## Constructors

### **TemplateLoaderFactory(IEnumerable&lt;ITemplateProvider&gt;, IMemoryCache)**

Initializes a new instance of the [TemplateLoaderFactory](./proffer.templating.internal.templateloaderfactory.md) class.

```csharp
public TemplateLoaderFactory(IEnumerable<ITemplateProvider> providers, IMemoryCache memoryCache)
```

#### Parameters

`providers` [IEnumerable&lt;ITemplateProvider&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The providers.

`memoryCache` IMemoryCache<br>
The memory cache.

## Methods

### **Create(IStore)**

Creates a template loader from the specified store.

```csharp
public ITemplateLoader Create(IStore store)
```

#### Parameters

`store` IStore<br>
The store.

#### Returns

[ITemplateLoader](./proffer.templating.itemplateloader.md)<br>

            A  that loads templates from the given .

### **Create(IStore, String)**

Creates a template loader from the specified store with the specified cache scope.

```csharp
public ITemplateLoader Create(IStore store, string scope)
```

#### Parameters

`store` IStore<br>
The store.

`scope` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The scope.

#### Returns

[ITemplateLoader](./proffer.templating.itemplateloader.md)<br>

            A  that loads templates from the given .
