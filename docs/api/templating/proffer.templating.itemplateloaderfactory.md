# ITemplateLoaderFactory

Namespace: Proffer.Templating

Creates [ITemplateLoader](./proffer.templating.itemplateloader.md) from Proffer.Storage.IStore.

```csharp
public interface ITemplateLoaderFactory
```

## Methods

### **Create(IStore)**

Creates a template loader from the specified store.

```csharp
ITemplateLoader Create(IStore store)
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
ITemplateLoader Create(IStore store, string scope)
```

#### Parameters

`store` IStore<br>
The store.

`scope` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache scope.

#### Returns

[ITemplateLoader](./proffer.templating.itemplateloader.md)<br>
A  that loads templates from the given .
