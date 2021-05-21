[`< Back`](./)

---

# FileExtendedProperties

Namespace: Proffer.Storage.FileSystem.Internal

Extends standard file properties to match the requirements of Proffer.Storage.IFileProperties.

```csharp
public class FileExtendedProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties)

## Properties

### **ContentType**

Gets or sets the content-type.

```csharp
public string ContentType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ETag**

Gets or sets the etag.

```csharp
public string ETag { get; set; }
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

Gets or sets the MD5 digest of the content.

```csharp
public string ContentMD5 { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Metadata**

Gets or sets the metadata.

```csharp
public IDictionary<string, string> Metadata { get; set; }
```

#### Property Value

[IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>

## Constructors

### **FileExtendedProperties()**

Initializes a new instance of the [FileExtendedProperties](./proffer.storage.filesystem.internal.fileextendedproperties) class.

```csharp
public FileExtendedProperties()
```

---

[`< Back`](./)
