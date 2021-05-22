# AzureBlobsFileProperties

Namespace: Proffer.Storage.Azure.Blobs.Internal

File common properties with metadata stored on Azure Blobs.

```csharp
public class AzureBlobsFileProperties : Proffer.Storage.IFileProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AzureBlobsFileProperties](./proffer.storage.azure.blobs.internal.azureblobsfileproperties)<br>
Implements IFileProperties

## Properties

### **LastModified**

Gets the last modified time.

```csharp
public Nullable<DateTimeOffset> LastModified { get; private set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Length**

Gets the length of the content.

```csharp
public long Length { get; private set; }
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
public string ETag { get; private set; }
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
public string ContentMD5 { get; private set; }
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

### **AzureBlobsFileProperties(BlobClient, BlobProperties)**

Initializes a new instance of the [AzureBlobsFileProperties](./proffer.storage.azure.blobs.internal.azureblobsfileproperties) class.

```csharp
public AzureBlobsFileProperties(BlobClient blobClient, BlobProperties blobProperties)
```

#### Parameters

`blobClient` BlobClient<br>
The Azure Blobs client.

`blobProperties` BlobProperties<br>
The blob properties.

### **AzureBlobsFileProperties(BlobClient, BlobItem)**

Initializes a new instance of the [AzureBlobsFileProperties](./proffer.storage.azure.blobs.internal.azureblobsfileproperties) class.

```csharp
public AzureBlobsFileProperties(BlobClient blobClient, BlobItem blobItem)
```

#### Parameters

`blobClient` BlobClient<br>
The Azure Blobs client.

`blobItem` BlobItem<br>
The blob item from listing.

## Methods

### **SaveAsync()**



```csharp
internal Task SaveAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
