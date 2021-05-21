# IFileProperties

Namespace: Proffer.Storage

File common properties with metadata.

```csharp
public interface IFileProperties
```

## Properties

### **LastModified**

Gets the last modified time.

```csharp
public abstract Nullable<DateTimeOffset> LastModified { get; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Length**

Gets the length of the content.

```csharp
public abstract long Length { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **ContentType**

Gets or sets the content-type.

```csharp
public abstract string ContentType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ETag**

Gets the etag.

```csharp
public abstract string ETag { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CacheControl**

Gets or sets the cache control.

```csharp
public abstract string CacheControl { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ContentMD5**

Gets the MD5 digest of the content.

```csharp
public abstract string ContentMD5 { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Metadata**

Gets the metadata.

```csharp
public abstract IDictionary<string, string> Metadata { get; }
```

#### Property Value

[IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
