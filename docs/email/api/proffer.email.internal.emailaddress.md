# EmailAddress

Namespace: Proffer.Email.Internal

A simple email address with its optional display name.

```csharp
public class EmailAddress : Proffer.Email.IEmailAddress
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EmailAddress](./proffer.email.internal.emailaddress)<br>
Implements [IEmailAddress](./proffer.email.iemailaddress)

## Properties

### **Email**

Gets or sets the email.

```csharp
public string Email { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DisplayName**

Gets or sets the display name.

```csharp
public string DisplayName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **EmailAddress()**

Initializes a new instance of the [EmailAddress](./proffer.email.internal.emailaddress) class.

```csharp
public EmailAddress()
```

### **EmailAddress(String, String)**

Initializes a new instance of the [EmailAddress](./proffer.email.internal.emailaddress) class.

```csharp
public EmailAddress(string email, string displayName)
```

#### Parameters

`email` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The email.

`displayName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The display name.
