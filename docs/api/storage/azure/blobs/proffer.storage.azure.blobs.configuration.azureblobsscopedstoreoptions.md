# AzureBlobsScopedStoreOptions

Namespace: Proffer.Storage.Azure.Blobs.Configuration

Options for a scoped [AzureBlobsStore](./proffer.storage.azure.blobs.azureblobsstore.md).

```csharp
public class AzureBlobsScopedStoreOptions : AzureBlobsStoreOptions, Proffer.Storage.Configuration.IStoreOptions, Proffer.Configuration.INamedElementOptions, Proffer.Azure.Configuration.IAzureStorageOptions, Proffer.Storage.Configuration.IScopedStoreOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → StoreOptions → [AzureBlobsStoreOptions](./proffer.storage.azure.blobs.configuration.azureblobsstoreoptions.md) → [AzureBlobsScopedStoreOptions](./proffer.storage.azure.blobs.configuration.azureblobsscopedstoreoptions.md)<br>
Implements IStoreOptions, INamedElementOptions, IAzureStorageOptions, IScopedStoreOptions

## Properties

### **FolderNameFormat**

Gets the folder name format.

```csharp
public string FolderNameFormat { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **AzureBlobsScopedStoreOptions()**



```csharp
public AzureBlobsScopedStoreOptions()
```
