[`< Back`](./)

---

# ITemplateProviderScope

Namespace: Proffer.Templating

Templating scope for a configured provider.

```csharp
public interface ITemplateProviderScope
```

## Methods

### **Compile(String)**

Compiles the specified template content.

```csharp
ITemplate Compile(string templateContent)
```

#### Parameters

`templateContent` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Content of the template.

#### Returns

[ITemplate](./proffer.templating.itemplate)<br>
A new  compiled from the content.

### **RegisterPartial(String, String)**

Registers a partial template with a name.

```csharp
void RegisterPartial(string name, string template)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name.

`template` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The template.

#### Exceptions

[NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception)<br>

---

[`< Back`](./)
