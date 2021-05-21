[`< Back`](./)

---

# BadStoreConfiguration

Namespace: Proffer.Storage.Exceptions

Thrown when a store was not properly configured.

```csharp
public class BadStoreConfiguration : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration)<br>
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### **Errors**

Gets the validation errors.

```csharp
public IEnumerable<IOptionError> Errors { get; }
```

#### Property Value

[IEnumerable&lt;IOptionError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **TargetSite**



```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **StackTrace**



```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Message**



```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**



```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**



```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**



```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**



```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**



```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **BadStoreConfiguration(String, String)**

Initializes a new instance of the [BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration) class.

```csharp
public BadStoreConfiguration(string storeName, string details)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`details` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The details.

### **BadStoreConfiguration(String, IEnumerable&lt;IOptionError&gt;)**

Initializes a new instance of the [BadStoreConfiguration](./proffer.storage.exceptions.badstoreconfiguration) class.

```csharp
public BadStoreConfiguration(string storeName, IEnumerable<IOptionError> errors)
```

#### Parameters

`storeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the store.

`errors` [IEnumerable&lt;IOptionError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The errors.

---

[`< Back`](./)
