[`< Back`](./)

---

# IEmailAttachment

Namespace: Proffer.Email

An email attachment file.

```csharp
public interface IEmailAttachment
```

## Properties

### **FileName**

Gets or sets the file name.

```csharp
public abstract string FileName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

Gets or sets the file content.

```csharp
public abstract Byte[] Data { get; set; }
```

#### Property Value

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **MediaType**

Gets or sets the media type.

```csharp
public abstract string MediaType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MediaSubtype**

Gets or sets the media subtype.

```csharp
public abstract string MediaSubtype { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ContentType**

Gets the content-type.

```csharp
public abstract string ContentType { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

---

[`< Back`](./)
