# SharedAccessPolicy

Namespace: Proffer.Storage

Represents a simple shared access policy, which specifies the start time, expiry time, and permissions for a shared access signature.

```csharp
public class SharedAccessPolicy : ISharedAccessPolicy
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SharedAccessPolicy](./proffer.storage.sharedaccesspolicy)<br>
Implements [ISharedAccessPolicy](./proffer.storage.isharedaccesspolicy)

## Properties

### **StartTime**

Gets or sets the start time.

```csharp
public Nullable<DateTimeOffset> StartTime { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ExpiryTime**

Gets or sets the expiry time.

```csharp
public Nullable<DateTimeOffset> ExpiryTime { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Permissions**

Gets or sets the permissions.

```csharp
public SharedAccessPermissions Permissions { get; set; }
```

#### Property Value

[SharedAccessPermissions](./proffer.storage.sharedaccesspermissions)<br>

## Constructors

### **SharedAccessPolicy()**



```csharp
public SharedAccessPolicy()
```
