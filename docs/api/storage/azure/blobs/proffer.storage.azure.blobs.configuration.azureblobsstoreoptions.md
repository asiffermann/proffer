# AzureBlobsStoreOptions

Namespace: Proffer.Storage.Azure.Blobs.Configuration

Options for an [AzureBlobsStore](./proffer.storage.azure.blobs.azureblobsstore.md).

```csharp
public class AzureBlobsStoreOptions : Proffer.Storage.Configuration.StoreOptions, Proffer.Storage.Configuration.IStoreOptions, Proffer.Configuration.INamedElementOptions, Proffer.Azure.Configuration.IAzureStorageOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StoreOptions → [AzureBlobsStoreOptions](./proffer.storage.azure.blobs.configuration.azureblobsstoreoptions.md)<br>
Implements IStoreOptions, INamedElementOptions, IAzureStorageOptions

## Properties

### **ConnectionString**

Gets or sets the connection string.

```csharp
public string ConnectionString { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConnectionStringName**

Gets or sets the name of the connection string to reference.

```csharp
public string ConnectionStringName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**



```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderName**



```csharp
public string ProviderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProviderType**



```csharp
public string ProviderType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AccessLevel**



```csharp
public AccessLevel AccessLevel { get; set; }
```

#### Property Value

AccessLevel<br>

### **FolderName**



```csharp
public string FolderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AzureBlobsStoreOptions()**



```csharp
public AzureBlobsStoreOptions()
```

## Methods

### **Validate(Boolean)**

Validates the options.

```csharp
public IEnumerable<IOptionError> Validate(bool throwOnError)
```

#### Parameters

`throwOnError` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, throws an exception when the validation fails with any .

#### Returns

[IEnumerable&lt;IOptionError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

            The  returned by the validation, if any.

#### Exceptions

Proffer.Storage.Exceptions.BadStoreConfiguration<br>
