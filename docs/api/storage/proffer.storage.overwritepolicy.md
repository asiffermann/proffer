# OverwritePolicy

Namespace: Proffer.Storage

Defines an overwrite policy when saving a file to a store.

```csharp
public enum OverwritePolicy
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [OverwritePolicy](./proffer.storage.overwritepolicy.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Always | 0 | Always overwrite. |
| IfContentModified | 1 | Overwrite only if the file content is modified. |
| Never | 2 | Never overwrite. |
