# PrivateFileReference

Namespace: Proffer.Storage.Internal

A simple reference of a stored file at a given path.

```csharp
public class PrivateFileReference : Proffer.Storage.IPrivateFileReference
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PrivateFileReference](./proffer.storage.internal.privatefilereference.md)<br>
Implements [IPrivateFileReference](./proffer.storage.iprivatefilereference.md)

## Properties

### **Path**

Gets the file path.

```csharp
public string Path { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **PrivateFileReference(String)**

Initializes a new instance of the [PrivateFileReference](./proffer.storage.internal.privatefilereference.md) class.

```csharp
public PrivateFileReference(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.