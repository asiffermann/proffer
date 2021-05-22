# FileSystemFileProperties

Namespace: Proffer.Storage.FileSystem.Internal

File common properties with metadata stored on a File System.

```csharp
public class FileSystemFileProperties : Proffer.Storage.IFileProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileSystemFileProperties](./proffer.storage.filesystem.internal.filesystemfileproperties.md)<br>
Implements IFileProperties

## Properties

### **LastModified**

Gets the last modified time.

```csharp
public Nullable<DateTimeOffset> LastModified { get; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Length**

Gets the length of the content.

```csharp
public long Length { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **ContentType**

Gets or sets the content-type.

```csharp
public string ContentType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ETag**

Gets the etag.

```csharp
public string ETag { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CacheControl**

Gets or sets the cache control.

```csharp
public string CacheControl { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ContentMD5**

Gets the MD5 digest of the content.

```csharp
public string ContentMD5 { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Metadata**

Gets the metadata.

```csharp
public IDictionary<string, string> Metadata { get; }
```

#### Property Value

[IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>

## Constructors

### **FileSystemFileProperties(String, FileExtendedProperties)**

Initializes a new instance of the [FileSystemFileProperties](./proffer.storage.filesystem.internal.filesystemfileproperties.md) class.

```csharp
public FileSystemFileProperties(string fileSystemPath, FileExtendedProperties extendedProperties)
```

#### Parameters

`fileSystemPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file system path.

`extendedProperties` [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties.md)<br>
The extended properties.
