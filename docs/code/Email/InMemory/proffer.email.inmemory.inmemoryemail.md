[`< Back`](./)

---

# InMemoryEmail

Namespace: Proffer.Email.InMemory

An object to retain the values of the email that would have been sent.

```csharp
public class InMemoryEmail
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [InMemoryEmail](./proffer.email.inmemory.inmemoryemail)

## Properties

### **Subject**

Gets or sets the subject.

```csharp
public string Subject { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MessageText**

Gets or sets the message as plain-text.

```csharp
public string MessageText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MessageHtml**

Gets or sets the message as HTML.

```csharp
public string MessageHtml { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **To**

Gets or sets the email recipients.

```csharp
public IEmailAddress[] To { get; set; }
```

#### Property Value

IEmailAddress[]<br>

### **Cc**

Gets or sets the email recipients.

```csharp
public IEmailAddress[] Cc { get; set; }
```

#### Property Value

IEmailAddress[]<br>

### **Bcc**

Gets or sets the BCC email recipients.

```csharp
public IEmailAddress[] Bcc { get; set; }
```

#### Property Value

IEmailAddress[]<br>

### **From**

Gets or sets the sender email address.

```csharp
public IEmailAddress From { get; set; }
```

#### Property Value

IEmailAddress<br>

### **ReplyTo**

Gets or sets the reply-to email address.

```csharp
public IEmailAddress ReplyTo { get; set; }
```

#### Property Value

IEmailAddress<br>

### **Attachments**

Gets or sets the attachments files.

```csharp
public IEnumerable<IEmailAttachment> Attachments { get; set; }
```

#### Property Value

[IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

## Constructors

### **InMemoryEmail()**



```csharp
public InMemoryEmail()
```

---

[`< Back`](./)
