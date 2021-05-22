# AccessLevel

Namespace: Proffer.Storage.Configuration

Defines a store access level.

```csharp
public enum AccessLevel
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [AccessLevel](./proffer.storage.configuration.accesslevel.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Private | 0 | No public access. |
| Confidential | 1 | Public files without listing. |
| Public | 2 | Public access. |
