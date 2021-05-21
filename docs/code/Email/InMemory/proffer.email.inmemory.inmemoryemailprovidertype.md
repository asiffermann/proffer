[`< Back`](./)

---

# InMemoryEmailProviderType

Namespace: Proffer.Email.InMemory

Builds [InMemoryEmailProvider](./proffer.email.inmemory.inmemoryemailprovider).

```csharp
public class InMemoryEmailProviderType : Proffer.Email.IEmailProviderType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [InMemoryEmailProviderType](./proffer.email.inmemory.inmemoryemailprovidertype)<br>
Implements IEmailProviderType

## Properties

### **Name**

Gets the name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **InMemoryEmailProviderType(IInMemoryEmailRepository)**

Initializes a new instance of the [InMemoryEmailProviderType](./proffer.email.inmemory.inmemoryemailprovidertype) class.

```csharp
public InMemoryEmailProviderType(IInMemoryEmailRepository inMemoryEmailRepository)
```

#### Parameters

`inMemoryEmailRepository` [IInMemoryEmailRepository](./proffer.email.inmemory.iinmemoryemailrepository)<br>
The in-memory email repository.

## Methods

### **BuildProvider(IEmailProviderOptions)**

Builds the provider.

```csharp
public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
```

#### Parameters

`providerOptions` IEmailProviderOptions<br>
The provider options.

#### Returns

IEmailProvider<br>

            A new .

---

[`< Back`](./)
