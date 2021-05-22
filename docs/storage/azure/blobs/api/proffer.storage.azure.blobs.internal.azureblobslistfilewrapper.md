# AzureBlobsListFileWrapper

Namespace: Proffer.Storage.Azure.Blobs.Internal

Represents a file in a being-listed [AzureBlobsStore](./proffer.storage.azure.blobs.azureblobsstore).

```csharp
public class AzureBlobsListFileWrapper : Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileInfoBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → FileSystemInfoBase → FileInfoBase → [AzureBlobsListFileWrapper](./proffer.storage.azure.blobs.internal.azureblobslistfilewrapper)

## Properties

### **FullName**

A string containing the full path of the file.

```csharp
public string FullName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

A string containing the name of the file.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ParentDirectory**

The parent directory for the current file.

```csharp
public DirectoryInfoBase ParentDirectory { get; }
```

#### Property Value

DirectoryInfoBase<br>

## Constructors

### **AzureBlobsListFileWrapper(AzureBlobsFileReference, AzureBlobsListDirectoryWrapper)**

Initializes a new instance of the [AzureBlobsListFileWrapper](./proffer.storage.azure.blobs.internal.azureblobslistfilewrapper) class.

```csharp
public AzureBlobsListFileWrapper(AzureBlobsFileReference file, AzureBlobsListDirectoryWrapper parent)
```

#### Parameters

`file` [AzureBlobsFileReference](./proffer.storage.azure.blobs.internal.azureblobsfilereference)<br>
The file reference.

`parent` [AzureBlobsListDirectoryWrapper](./proffer.storage.azure.blobs.internal.azureblobslistdirectorywrapper)<br>
The parent directory.
