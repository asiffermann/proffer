[`< Back`](./)

---

# ConfigureProviderOptions&lt;TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions&gt;

Namespace: Proffer.Storage.Internal

Configures a provider  from generic [StorageOptions](./proffer.storage.configuration.storageoptions).

```csharp
public class ConfigureProviderOptions<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions> : 
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

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ConfigureProviderOptions&lt;TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions&gt;](./proffer.storage.internal.configureprovideroptions-4)<br>
Implements IConfigureOptions&lt;TParsedOptions&gt;

## Constructors

### **ConfigureProviderOptions(IOptions&lt;StorageOptions&gt;)**

Initializes a new instance of the [ConfigureProviderOptions&lt;TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions&gt;](./proffer.storage.internal.configureprovideroptions-4) class.

```csharp
public ConfigureProviderOptions(IOptions<StorageOptions> storageOptions)
```

#### Parameters

`storageOptions` IOptions&lt;StorageOptions&gt;<br>
The storage options.

## Methods

### **Configure(TParsedOptions)**

Invoked to configure a  instance.

```csharp
public void Configure(TParsedOptions options)
```

#### Parameters

`options` TParsedOptions<br>
The options instance to configure.

#### Exceptions

[BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration)<br>

[BadProviderConfiguration](./proffer.storage.exceptions.badproviderconfiguration)<br>

---

[`< Back`](./)
