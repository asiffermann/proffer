# IStoreOptions

Namespace: Proffer.Storage.Configuration

Options for an [IStore](./proffer.storage.istore).

```csharp
public interface IStoreOptions : Proffer.Configuration.INamedElementOptions
```

Implements INamedElementOptions

## Properties

### **ProviderName**

Gets or sets the name of the provider.

```csharp
public abstract string ProviderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderType**

Gets or sets the type of the provider.

```csharp
public abstract string ProviderType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AccessLevel**

Gets or sets the access level.

```csharp
public abstract AccessLevel AccessLevel { get; set; }
```

#### Property Value

[AccessLevel](./proffer.storage.configuration.accesslevel)<br>

### **FolderName**

Gets or sets the name of the folder.

```csharp
public abstract string FolderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **Validate(Boolean)**

Validates the options.

```csharp
IEnumerable<IOptionError> Validate(bool throwOnError)
```

#### Parameters

`throwOnError` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, throws an exception when the validation fails with any .

#### Returns

[IEnumerable&lt;IOptionError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The  returned by the validation, if any.

#### Exceptions

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration)<br>
