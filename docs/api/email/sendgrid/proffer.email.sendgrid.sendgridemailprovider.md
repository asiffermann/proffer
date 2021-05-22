# SendGridEmailProvider

Namespace: Proffer.Email.SendGrid

A provider that sends email using N:SendGrid API.

```csharp
public class SendGridEmailProvider : Proffer.Email.IEmailProvider
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SendGridEmailProvider](./proffer.email.sendgrid.sendgridemailprovider.md)<br>
Implements IEmailProvider

## Constructors

### **SendGridEmailProvider(IEmailProviderOptions)**

Initializes a new instance of the [SendGridEmailProvider](./proffer.email.sendgrid.sendgridemailprovider.md) class.

```csharp
public SendGridEmailProvider(IEmailProviderOptions options)
```

#### Parameters

`options` IEmailProviderOptions<br>
The options.

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
apiKey

## Methods

### **SendEmailAsync(IEmailAddress, IEnumerable&lt;IEmailAddress&gt;, String, String, String)**

Sends an email.

```csharp
public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml)
```

#### Parameters

`from` IEmailAddress<br>
The sender email address.

`recipients` [IEnumerable&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The email recipients.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`bodyText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`bodyHtml` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as HTML.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, IEnumerable&lt;IEmailAddress&gt;, String, String, String, IEnumerable&lt;IEmailAttachment&gt;)**

Sends an email.

```csharp
public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments)
```

#### Parameters

`from` IEmailAddress<br>
The sender email address.

`recipients` [IEnumerable&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The email recipients.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`bodyText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`bodyHtml` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as HTML.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

            A task that represents the asynchronous operation.

### **SendEmailAsync(IEmailAddress, IEnumerable&lt;IEmailAddress&gt;, IEnumerable&lt;IEmailAddress&gt;, IEnumerable&lt;IEmailAddress&gt;, String, String, String, IEnumerable&lt;IEmailAttachment&gt;, IEmailAddress)**

Sends an email.

```csharp
public Task SendEmailAsync(IEmailAddress from, IEnumerable<IEmailAddress> recipients, IEnumerable<IEmailAddress> ccRecipients, IEnumerable<IEmailAddress> bccRecipients, string subject, string bodyText, string bodyHtml, IEnumerable<IEmailAttachment> attachments, IEmailAddress replyTo)
```

#### Parameters

`from` IEmailAddress<br>
The sender email address.

`recipients` [IEnumerable&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The email recipients.

`ccRecipients` [IEnumerable&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The CC email recipients.

`bccRecipients` [IEnumerable&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The BCC email recipients.

`subject` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The subject.

`bodyText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as plain text.

`bodyHtml` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The body as HTML.

`attachments` [IEnumerable&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The file attachments.

`replyTo` IEmailAddress<br>
The reply-to email address.

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Each email address should be unique between to, cc, and bcc recipients. We found duplicates.

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Cannot Send Email: {response.StatusCode}
