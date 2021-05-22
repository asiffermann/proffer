# IScopedStoreOptions

Namespace: Proffer.Storage.Configuration

Options for a scoped [IStore](./proffer.storage.istore.md).

```csharp
public interface IScopedStoreOptions : IStoreOptions, Proffer.Configuration.INamedElementOptions
```

Implements [IStoreOptions](./proffer.storage.configuration.istoreoptions.md), INamedElementOptions

## Properties

### **FolderNameFormat**

Gets the folder name format.

```csharp
public abstract string FolderNameFormat { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>