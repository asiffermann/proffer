# SharedAccessPermissions

Namespace: Proffer.Storage

Specifies the set of possible permissions for a shared access policy.

```csharp
public enum SharedAccessPermissions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [SharedAccessPermissions](./proffer.storage.sharedaccesspermissions)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | No shared access granted. |
| Read | 1 | Read access granted. |
| Write | 2 | Write access granted. |
| Delete | 4 | Delete access granted. |
| List | 8 | List access granted. |
| Add | 16 | Add access granted. |
| Create | 32 | Create access granted. |
