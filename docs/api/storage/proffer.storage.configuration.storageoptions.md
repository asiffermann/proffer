# StorageOptions

Namespace: Proffer.Storage.Configuration

The Proffer.Storage options with providers and stores.

```csharp
public class StorageOptions : IParsedOptions`3
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StorageOptions](./proffer.storage.configuration.storageoptions.md)<br>
Implements [IParsedOptions&lt;ProviderOptions, StoreOptions, ScopedStoreOptions&gt;](./proffer.storage.configuration.iparsedoptions-3.md)

## Fields

### **DefaultConfigurationSectionName**

The default configuration section name.

```csharp
public static string DefaultConfigurationSectionName;
```

## Properties

### **Name**

Gets the name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Providers**

Gets or sets the providers unparsed options.

```csharp
public IReadOnlyDictionary<string, IConfigurationSection> Providers { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, IConfigurationSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **Stores**

Gets or sets the stores unparsed options.

```csharp
public IReadOnlyDictionary<string, IConfigurationSection> Stores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, IConfigurationSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ScopedStores**

Gets or sets the scoped stores unparsed options.

```csharp
public IReadOnlyDictionary<string, IConfigurationSection> ScopedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, IConfigurationSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

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
public IReadOnlyDictionary<string, ProviderOptions> ParsedProviders { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, ProviderOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedStores**

Gets or sets the parsed stores options.

```csharp
public IReadOnlyDictionary<string, StoreOptions> ParsedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, StoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedScopedStores**

Gets or sets the parsed scoped stores options.

```csharp
public IReadOnlyDictionary<string, ScopedStoreOptions> ParsedScopedStores { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, ScopedStoreOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **StorageOptions()**

Initializes a new instance of the [StorageOptions](./proffer.storage.configuration.storageoptions.md) class.

```csharp
public StorageOptions()
```

## Methods

### **BindProviderOptions(ProviderOptions)**

Binds the provider instance options.

```csharp
public void BindProviderOptions(ProviderOptions providerInstanceOptions)
```

#### Parameters

`providerInstanceOptions` ProviderOptions<br>
The provider instance options.

### **BindStoreOptions(StoreOptions, ProviderOptions)**

Binds the store options.

```csharp
public void BindStoreOptions(StoreOptions storeOptions, ProviderOptions providerInstanceOptions)
```

#### Parameters

`storeOptions` [StoreOptions](./proffer.storage.configuration.storeoptions.md)<br>
The store options.

`providerInstanceOptions` ProviderOptions<br>
The provider instance options.
