# IParsedOptions&lt;TProviderOptions, TStoreOptions, TScopedStoreOptions&gt;

Namespace: Proffer.Storage.Configuration

Typed options parsed from the dynamic configuration.

```csharp
public interface IParsedOptions<TProviderOptions, TStoreOptions, TScopedStoreOptions>
```

#### Type Parameters

`TProviderOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

## Properties

### **Name**

Gets the name.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConnectionStrings**

Gets or sets the connection strings.

```csharp
public abstract IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }
```

#### Property Value

[IReadOnlyDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **ParsedProviders**

Gets or sets the parsed provider options.

```csharp
public abstract IReadOnlyDictionary<string, TProviderOptions> ParsedProviders { get; set; }
```

#### Property Value

IReadOnlyDictionary&lt;String, TProviderOptions&gt;<br>

### **ParsedStores**

Gets or sets the parsed stores options.

```csharp
public abstract IReadOnlyDictionary<string, TStoreOptions> ParsedStores { get; set; }
```

#### Property Value

IReadOnlyDictionary&lt;String, TStoreOptions&gt;<br>

### **ParsedScopedStores**

Gets or sets the parsed scoped stores options.

```csharp
public abstract IReadOnlyDictionary<string, TScopedStoreOptions> ParsedScopedStores { get; set; }
```

#### Property Value

IReadOnlyDictionary&lt;String, TScopedStoreOptions&gt;<br>

## Methods

### **BindProviderOptions(TProviderOptions)**

Binds the provider options.

```csharp
void BindProviderOptions(TProviderOptions providerOptions)
```

#### Parameters

`providerOptions` TProviderOptions<br>
The provider options.

### **BindStoreOptions(TStoreOptions, TProviderOptions)**

Binds the store options.

```csharp
void BindStoreOptions(TStoreOptions storeOptions, TProviderOptions providerInstanceOptions)
```

#### Parameters

`storeOptions` TStoreOptions<br>
The store options.

`providerInstanceOptions` TProviderOptions<br>
The provider instance options.

#### Exceptions

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration)<br>
