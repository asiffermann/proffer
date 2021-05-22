# AzureBlobsProviderOptions

Namespace: Proffer.Storage.Azure.Blobs.Configuration

Options for an [AzureBlobsStorageProvider](./proffer.storage.azure.blobs.azureblobsstorageprovider.md).

```csharp
public class AzureBlobsProviderOptions : Proffer.Configuration.ProviderOptions, Proffer.Configuration.IProviderOptions, Proffer.Configuration.INamedElementOptions, Proffer.Azure.Configuration.IAzureStorageOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ProviderOptions → [AzureBlobsProviderOptions](./proffer.storage.azure.blobs.configuration.azureblobsprovideroptions.md)<br>
Implements IProviderOptions, INamedElementOptions, IAzureStorageOptions

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

### **Type**



```csharp
public string Type { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AzureBlobsProviderOptions()**



```csharp
public AzureBlobsProviderOptions()
```
