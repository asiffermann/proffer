# TemplateCollectionBase

Namespace: Proffer.Templating

Abstract base class to load and apply templates from a named Proffer.Storage.IStore.

```csharp
public abstract class TemplateCollectionBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TemplateCollectionBase](./proffer.templating.templatecollectionbase)

## Properties

### **Loader**

Gets the templates loader.

```csharp
public ITemplateLoader Loader { get; }
```

#### Property Value

[ITemplateLoader](./proffer.templating.itemplateloader)<br>

## Constructors

### **TemplateCollectionBase(String, IStorageFactory, ITemplateLoaderFactory)**

Initializes a new instance of the [TemplateCollectionBase](./proffer.templating.templatecollectionbase) class.

```csharp
public TemplateCollectionBase(string storeName, IStorageFactory storageFactory, ITemplateLoaderFactory templateLoaderFactory)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`storageFactory` IStorageFactory<br>
The storage factory.

`templateLoaderFactory` [ITemplateLoaderFactory](./proffer.templating.itemplateloaderfactory)<br>
The template loader factory.

## Methods

### **LoadAndApplyTemplate(String, Object)**

Loads the template and applies context.

```csharp
protected Task<string> LoadAndApplyTemplate(string templatePath, object context)
```

#### Parameters

`templatePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template path.

`context` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The context.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The templated result.

### **LoadAndApplyTemplate(String, Object, IFormatProvider)**

Loads the template and applies context.

```csharp
protected Task<string> LoadAndApplyTemplate(string templatePath, object context, IFormatProvider formatProvider)
```

#### Parameters

`templatePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template path.

`context` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The context.

`formatProvider` [IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/system.iformatprovider)<br>
The format provider.

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

            The templated result.
