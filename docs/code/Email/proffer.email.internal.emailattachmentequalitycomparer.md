[`< Back`](./)

---

# EmailAttachmentEqualityComparer

Namespace: Proffer.Email.Internal

Supports the comparison of [IEmailAttachment](./proffer.email.iemailattachment) for equality.

```csharp
public class EmailAttachmentEqualityComparer : Proffer.EqualityComparerBase`1[[Proffer.Email.IEmailAttachment, Proffer.Email, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEqualityComparer, System.Collections.Generic.IEqualityComparer`1[[Proffer.Email.IEmailAttachment, Proffer.Email, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EqualityComparer&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.equalitycomparer-1) → EqualityComparerBase&lt;IEmailAttachment&gt; → [EmailAttachmentEqualityComparer](./proffer.email.internal.emailattachmentequalitycomparer)<br>
Implements [IEqualityComparer](https://docs.microsoft.com/en-us/dotnet/api/system.collections.iequalitycomparer), [IEqualityComparer&lt;IEmailAttachment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1)

## Constructors

### **EmailAttachmentEqualityComparer()**



```csharp
public EmailAttachmentEqualityComparer()
```

## Methods

### **GetEqualityComponents(IEmailAttachment)**

Gets the object's components that participate in equality comparisons.

```csharp
protected IEnumerable<object> GetEqualityComponents(IEmailAttachment obj)
```

#### Parameters

`obj` [IEmailAttachment](./proffer.email.iemailattachment)<br>
The object.

#### Returns

[IEnumerable&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

            An enumerable containing the properties values.

---

[`< Back`](./)
