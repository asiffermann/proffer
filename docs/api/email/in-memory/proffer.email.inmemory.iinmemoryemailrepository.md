# IInMemoryEmailRepository

Namespace: Proffer.Email.InMemory

A repository in memory to hold sent emails through the provider.

```csharp
public interface IInMemoryEmailRepository
```

## Properties

### **Store**

Gets the emails store.

```csharp
public abstract IReadOnlyCollection<InMemoryEmail> Store { get; }
```

#### Property Value

[IReadOnlyCollection&lt;InMemoryEmail&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

## Methods

### **Save(InMemoryEmail)**

Saves the specified email in the store.

```csharp
void Save(InMemoryEmail email)
```

#### Parameters

`email` [InMemoryEmail](./proffer.email.inmemory.inmemoryemail.md)<br>
The email.
