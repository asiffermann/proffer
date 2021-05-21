[`< Back`](./)

---

# IEmailSender

Namespace: Proffer.Email

Sends templated or raw emails using configured providers.

```csharp
public interface IEmailSender
```

## Methods

### **SendEmailAsync(String, String, IEmailAddress[])**

Sends an email.

```csharp
Task SendEmailAsync(string subject, string message, IEmailAddress[] to)
```

#### Parameters

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, String, String, IEmailAddress[])**

Sends an email.

```csharp
Task SendEmailAsync(IEmailAddress from, string subject, string message, IEmailAddress[] to)
```

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, IEmailAddress, String, String, Boolean, IEmailAddress[])**

Sends an email.

```csharp
Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, IEmailAddress[] to)
```

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`plainTextOnly` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true the body shoud be sent as plain text only.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, String, String, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[])**

Sends an email.

```csharp
Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to)
```

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, IEmailAddress, String, String, Boolean, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[])**

Sends an email.

```csharp
Task SendEmailAsync(IEmailAddress from, IEmailAddress replyTo, string subject, string message, bool plainTextOnly, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to)
```

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`plainTextOnly` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true the body shoud be sent as plain text only.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, String, String, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[], IEmailAddress[], IEmailAddress[], IEmailAddress, Boolean)**

Sends an email.

```csharp
Task SendEmailAsync(IEmailAddress from, string subject, string message, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo, bool plainTextOnly)
```

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

`cc` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The CC email recipients.

`bcc` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The BCC email recipients.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

`plainTextOnly` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If set to true the body shoud be sent as plain text only.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(String, T, IEmailAddress[])**

Sends a templated email from the configured default sender email address.

```csharp
Task SendTemplatedEmailAsync<T>(string templateKey, T context, IEmailAddress[] to)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(IEmailAddress, String, T, IEmailAddress[])**

Sends a templated email.

```csharp
Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEmailAddress[] to)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(IEmailAddress, IEmailAddress, String, T, IEmailAddress[])**

Sends a templated email.

```csharp
Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, IEmailAddress[] to)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(IEmailAddress, String, T, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[])**

Sends a templated email.

```csharp
Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(IEmailAddress, IEmailAddress, String, T, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[])**

Sends a templated email.

```csharp
Task SendTemplatedEmailAsync<T>(IEmailAddress from, IEmailAddress replyTo, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendTemplatedEmailAsync&lt;T&gt;(IEmailAddress, String, T, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress[], IEmailAddress[], IEmailAddress[], IEmailAddress)**

Sends a templated email.

```csharp
Task SendTemplatedEmailAsync<T>(IEmailAddress from, string templateKey, T context, IEnumerable<IEmailAttachment> attachments, IEmailAddress[] to, IEmailAddress[] cc, IEmailAddress[] bcc, IEmailAddress replyTo)
```

#### Type Parameters

`T`<br>
The type of context to apply on the template.

#### Parameters

`from` [IEmailAddress](./proffer.email.iemailaddress)<br>
The sender email address.

`templateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template key.

`context` T<br>
The context  to apply on the template.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`to` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The email recipients.

`cc` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The CC email recipients.

`bcc` [IEmailAddress[]](./proffer.email.iemailaddress)<br>
The BCC email recipients.

`replyTo` [IEmailAddress](./proffer.email.iemailaddress)<br>
The reply-to email address.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

---

[`< Back`](./)
