[`< Back`](./)

---

# EmailAttachment

Namespace: Proffer.Email.Internal

A simpl email attachment file.

```csharp
public class EmailAttachment : Proffer.Email.IEmailAttachment
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EmailAttachment](./proffer.email.internal.emailattachment)<br>
Implements [IEmailAttachment](./proffer.email.iemailattachment)

## Properties

### **FileName**

Gets or sets the file name.

```csharp
public string FileName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

Gets or sets the file content.

```csharp
public Byte[] Data { get; set; }
```

#### Property Value

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **MediaType**

Gets or sets the media type.

```csharp
public string MediaType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MediaSubtype**

Gets or sets the media subtype.

```csharp
public string MediaSubtype { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ContentType**

Gets the content-type.

```csharp
public string ContentType { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **EmailAttachment()**

Initializes a new instance of the [EmailAttachment](./proffer.email.internal.emailattachment) class.

```csharp
public EmailAttachment()
```

### **EmailAttachment(String, Byte[], String)**

Initializes a new instance of the [EmailAttachment](./proffer.email.internal.emailattachment) class.

```csharp
public EmailAttachment(string fileName, Byte[] data, string contentType)
```

#### Parameters

`fileName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name.

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The file content.

`contentType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content-type.

### **EmailAttachment(String, Byte[], String, String)**

Initializes a new instance of the [EmailAttachment](./proffer.email.internal.emailattachment) class.

```csharp
public EmailAttachment(string fileName, Byte[] data, string mediaType, string mediaSubtype)
```

#### Parameters

`fileName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name.

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The file content.

`mediaType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The media type.

`mediaSubtype` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The media subtype.

---

[`< Back`](./)
