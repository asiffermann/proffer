[`< Back`](./)

---

# ISharedAccessPolicy

Namespace: Proffer.Storage

Represents a shared access policy, which specifies the start time, expiry time, and permissions for a shared access signature.

```csharp
public interface ISharedAccessPolicy
```

## Properties

### **StartTime**

Gets the start time.

```csharp
public abstract Nullable<DateTimeOffset> StartTime { get; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ExpiryTime**

Gets the expiry time.

```csharp
public abstract Nullable<DateTimeOffset> ExpiryTime { get; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Permissions**

Gets the permissions.

```csharp
public abstract SharedAccessPermissions Permissions { get; }
```

#### Property Value

[SharedAccessPermissions](./proffer.storage.sharedaccesspermissions)<br>

---

[`< Back`](./)
