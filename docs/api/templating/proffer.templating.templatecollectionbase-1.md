# TemplateCollectionBase&lt;TStore&gt;

Namespace: Proffer.Templating

Abstract base class to load and apply templates from a typed .

```csharp
public abstract class TemplateCollectionBase<TStore> : TemplateCollectionBase
```

#### Type Parameters

`TStore`<br>
The type of the store.

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TemplateCollectionBase](./proffer.templating.templatecollectionbase.md) → [TemplateCollectionBase&lt;TStore&gt;](./proffer.templating.templatecollectionbase-1.md)

## Properties

### **Loader**

Gets the templates loader.

```csharp
public ITemplateLoader Loader { get; }
```

#### Property Value

[ITemplateLoader](./proffer.templating.itemplateloader.md)<br>

## Constructors

### **TemplateCollectionBase(TStore, ITemplateLoaderFactory)**

Initializes a new instance of the [TemplateCollectionBase&lt;TStore&gt;](./proffer.templating.templatecollectionbase-1.md) class.

```csharp
public TemplateCollectionBase(TStore store, ITemplateLoaderFactory templateLoaderFactory)
```

#### Parameters

`store` TStore<br>
The typed store.

`templateLoaderFactory` [ITemplateLoaderFactory](./proffer.templating.itemplateloaderfactory.md)<br>
The template loader factory.
