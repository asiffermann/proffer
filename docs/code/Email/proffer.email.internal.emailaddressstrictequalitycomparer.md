[`< Back`](./)

---

# EmailAddressStrictEqualityComparer

Namespace: Proffer.Email.Internal

Supports the comparison of [IEmailAddress](./proffer.email.iemailaddress) for equality when both the email address and the display name should match.

```csharp
public class EmailAddressStrictEqualityComparer : Proffer.EqualityComparerBase`1[[Proffer.Email.IEmailAddress, Proffer.Email, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEqualityComparer, System.Collections.Generic.IEqualityComparer`1[[Proffer.Email.IEmailAddress, Proffer.Email, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EqualityComparer&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.equalitycomparer-1) → EqualityComparerBase&lt;IEmailAddress&gt; → [EmailAddressStrictEqualityComparer](./proffer.email.internal.emailaddressstrictequalitycomparer)<br>
Implements [IEqualityComparer](https://docs.microsoft.com/en-us/dotnet/api/system.collections.iequalitycomparer), [IEqualityComparer&lt;IEmailAddress&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1)

## Constructors

### **EmailAddressStrictEqualityComparer()**



```csharp
public EmailAddressStrictEqualityComparer()
```

## Methods

### **GetEqualityComponents(IEmailAddress)**

Gets the object's components that participate in equality comparisons.

```csharp
protected IEnumerable<object> GetEqualityComponents(IEmailAddress obj)
```

#### Parameters

`obj` [IEmailAddress](./proffer.email.iemailaddress)<br>
The object.

#### Returns

[IEnumerable&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

            An enumerable containing the properties values.

---

[`< Back`](./)
