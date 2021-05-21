# AzureBlobsParsedOptions

Namespace: Proffer.Storage.Azure.Blobs.Configuration

Typed Azure Blobs options parsed from the dynamic configuration.

```csharp
public class AzureBlobsParsedOptions : Proffer.Storage.Configuration.IParsedOptions`3[[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsProviderOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsStoreOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[Proffer.Storage.Azure.Blobs.Configuration.AzureBlobsScopedStoreOptions, Proffer.Storage.Azure.Blobs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AzureBlobsParsedOptions](./proffer.storage.azure.blobs.configuration.azureblobsparsedoptions)<br>
Implements IParsedOptions&lt;AzureBlobsProviderOptions, AzureBlobsStoreOptions, AzureBlobsScopedStoreOptions&gt;

## Properties

### **Name**

Gets the name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConnectionStrings**

Gets or sets the connection strings.

```csharp
public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedProviders**

Gets or sets the parsed provider instances options.

```csharp
public IReadOnlyDictionary<string, AzureBlobsProviderOptions> ParsedProviders { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, AzureBlobsProviderOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedStores**

Gets or sets the parsed stores options.

```csharp
public IReadOnlyDictionary<string, AzureBlobsStoreOptions> ParsedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, AzureBlobsStoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedScopedStores**

Gets or sets the parsed scoped stores options.

```csharp
public IReadOnlyDictionary<string, AzureBlobsScopedStoreOptions> ParsedScopedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, AzureBlobsScopedStoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **AzureBlobsParsedOptions()**



```csharp
public AzureBlobsParsedOptions()
```

## Methods

### **BindProviderOptions(AzureBlobsProviderOptions)**

Binds the provider instance options.

```csharp
public void BindProviderOptions(AzureBlobsProviderOptions providerInstanceOptions)
```

#### Parameters

`providerInstanceOptions` [AzureBlobsProviderOptions](./proffer.storage.azure.blobs.configuration.azureblobsprovideroptions)<br>
The provider instance options.

#### Exceptions

Proffer.Storage.Exceptions.BadProviderConfiguration<br>

### **BindStoreOptions(AzureBlobsStoreOptions, AzureBlobsProviderOptions)**

Binds the store options.

```csharp
public void BindStoreOptions(AzureBlobsStoreOptions storeOptions, AzureBlobsProviderOptions providerInstanceOptions)
```

#### Parameters

`storeOptions` [AzureBlobsStoreOptions](./proffer.storage.azure.blobs.configuration.azureblobsstoreoptions)<br>
The store options.

`providerInstanceOptions` [AzureBlobsProviderOptions](./proffer.storage.azure.blobs.configuration.azureblobsprovideroptions)<br>
The provider instance options.

#### Exceptions

Proffer.Storage.Exceptions.BadStoreConfiguration<br>
