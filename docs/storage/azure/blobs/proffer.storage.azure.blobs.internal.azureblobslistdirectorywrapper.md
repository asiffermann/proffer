# AzureBlobsListDirectoryWrapper

Namespace: Proffer.Storage.Azure.Blobs.Internal

Represents a directory in a being-listed [AzureBlobsStore](./proffer.storage.azure.blobs.azureblobsstore).

```csharp
public class AzureBlobsListDirectoryWrapper : Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → FileSystemInfoBase → DirectoryInfoBase → [AzureBlobsListDirectoryWrapper](./proffer.storage.azure.blobs.internal.azureblobslistdirectorywrapper)

## Properties

### **FullName**

A string containing the full path of the directory.

```csharp
public string FullName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

A string containing the name of the directory.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ParentDirectory**

The parent directory for the current directory.

```csharp
public DirectoryInfoBase ParentDirectory { get; }
```

#### Property Value

DirectoryInfoBase<br>

## Constructors

### **AzureBlobsListDirectoryWrapper(FileSystemInfoBase)**

Initializes a new instance of the [AzureBlobsListDirectoryWrapper](./proffer.storage.azure.blobs.internal.azureblobslistdirectorywrapper) class.

```csharp
public AzureBlobsListDirectoryWrapper(FileSystemInfoBase childrens)
```

#### Parameters

`childrens` FileSystemInfoBase<br>
The childrens.

### **AzureBlobsListDirectoryWrapper(String, Dictionary&lt;String, AzureBlobsFileReference&gt;)**

Initializes a new instance of the [AzureBlobsListDirectoryWrapper](./proffer.storage.azure.blobs.internal.azureblobslistdirectorywrapper) class.

```csharp
public AzureBlobsListDirectoryWrapper(string path, Dictionary<string, AzureBlobsFileReference> files)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`files` [Dictionary&lt;String, AzureBlobsFileReference&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The files.

## Methods

### **EnumerateFileSystemInfos()**

Enumerates all files and directories in the directory.

```csharp
public IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos()
```

#### Returns

[IEnumerable&lt;FileSystemInfoBase&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

            Collection of files and directories

### **GetDirectory(String)**

Returns an instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase that represents a subdirectory.

```csharp
public DirectoryInfoBase GetDirectory(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory name

#### Returns

DirectoryInfoBase<br>

            Instance of  even if directory does not exist

#### Exceptions

[NotImplementedException](https://docs.microsoft.com/en-us/dotnet/api/system.notimplementedexception)<br>

### **GetFile(String)**

Returns an instance of Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileInfoBase that represents a file in the directory.

```csharp
public FileInfoBase GetFile(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name

#### Returns

FileInfoBase<br>

            Instance of  even if file does not exist
