# InMemoryEmailRepository

Namespace: Proffer.Email.InMemory

A repository in memory to hold sent emails through the provider.

```csharp
public class InMemoryEmailRepository : IInMemoryEmailRepository
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [InMemoryEmailRepository](./proffer.email.inmemory.inmemoryemailrepository.md)<br>
Implements [IInMemoryEmailRepository](./proffer.email.inmemory.iinmemoryemailrepository.md)

## Properties

### **Store**

Gets the emails store.

```csharp
public IReadOnlyCollection<InMemoryEmail> Store { get; }
```

#### Property Value

[IReadOnlyCollection&lt;InMemoryEmail&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

## Constructors

### **InMemoryEmailRepository()**



```csharp
public InMemoryEmailRepository()
```

## Methods

### **Save(InMemoryEmail)**

Saves the specified email in the store.

```csharp
public void Save(InMemoryEmail email)
```

#### Parameters

`email` [InMemoryEmail](./proffer.email.inmemory.inmemoryemail.md)<br>
The email.
