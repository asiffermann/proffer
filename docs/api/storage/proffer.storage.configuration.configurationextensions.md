# ConfigurationExtensions

Namespace: Proffer.Storage.Configuration

Extensions methods to parse and bind options.

```csharp
public static class ConfigurationExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigurationExtensions](./proffer.storage.configuration.configurationextensions.md)

## Methods

### **Parse&lt;TOptions&gt;(IReadOnlyDictionary&lt;String, IConfigurationSection&gt;)**

Parses the specified unparsed configuration.

```csharp
public static IReadOnlyDictionary<string, TOptions> Parse<TOptions>(IReadOnlyDictionary<string, IConfigurationSection> unparsedConfiguration)
```

#### Type Parameters

`TOptions`<br>
The type of the options.

#### Parameters

`unparsedConfiguration` [IReadOnlyDictionary&lt;String, IConfigurationSection&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>
The unparsed configuration.

#### Returns

IReadOnlyDictionary&lt;String, TOptions&gt;<br>
A typed dictionary with options binding from the given unparsed configuration.

### **GetStoreConfiguration&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;(IParsedOptions&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;, String, Boolean)**

Gets the store configuration.

```csharp
public static TStoreOptions GetStoreConfiguration<TInstanceOptions, TStoreOptions, TScopedStoreOptions>(IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions> parsedOptions, string storeName, bool throwIfNotFound)
```

#### Type Parameters

`TInstanceOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

#### Parameters

`parsedOptions` IParsedOptions&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;<br>
The parsed options.

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`throwIfNotFound` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, throws an exception if the store configuration is not found.

#### Returns

TStoreOptions<br>
The typed store configuration.

#### Exceptions

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **GetScopedStoreConfiguration&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;(IParsedOptions&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;, String, Boolean)**

Gets the scoped store configuration.

```csharp
public static TScopedStoreOptions GetScopedStoreConfiguration<TInstanceOptions, TStoreOptions, TScopedStoreOptions>(IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions> parsedOptions, string storeName, bool throwIfNotFound)
```

#### Type Parameters

`TInstanceOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

#### Parameters

`parsedOptions` IParsedOptions&lt;TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;<br>
The parsed options.

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`throwIfNotFound` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true, throws an exception if the store configuration is not found.

#### Returns

TScopedStoreOptions<br>
The typed scoped store configuration.

#### Exceptions

[StoreNotFoundException](./proffer.storage.exceptions.storenotfoundexception.md)<br>

### **Compute&lt;TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions&gt;(TProviderOptions, TParsedOptions)**

Computes the specified options.

```csharp
public static void Compute<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions>(TProviderOptions providerOptions, TParsedOptions options)
```

#### Type Parameters

`TParsedOptions`<br>
The type of the parsed options.

`TProviderOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

#### Parameters

`providerOptions` TProviderOptions<br>
The provider options.

`options` TParsedOptions<br>
The options.

#### Exceptions

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration.md)<br>

### **Compute&lt;TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;(TStoreOptions, TParsedOptions)**

Computes the specified options.

```csharp
public static void Compute<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(TStoreOptions parsedStore, TParsedOptions options)
```

#### Type Parameters

`TParsedOptions`<br>
The type of the parsed options.

`TInstanceOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

#### Parameters

`parsedStore` TStoreOptions<br>
The parsed store options.

`options` TParsedOptions<br>
The options.

#### Exceptions

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration.md)<br>

### **ParseStoreOptions&lt;TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions&gt;(IStoreOptions, TParsedOptions)**

Parses the store options.

```csharp
public static TStoreOptions ParseStoreOptions<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(IStoreOptions storeOptions, TParsedOptions options)
```

#### Type Parameters

`TParsedOptions`<br>
The type of the parsed options.

`TInstanceOptions`<br>
The type of the provider instance options.

`TStoreOptions`<br>
The type of the store options.

`TScopedStoreOptions`<br>
The type of the scoped store options.

#### Parameters

`storeOptions` [IStoreOptions](./proffer.storage.configuration.istoreoptions.md)<br>
The store options.

`options` TParsedOptions<br>
The options.

#### Returns

TStoreOptions<br>
The parsed store options.
