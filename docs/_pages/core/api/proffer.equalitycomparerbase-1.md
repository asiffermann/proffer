# EqualityComparerBase&lt;T&gt;

Namespace: Proffer

Implements [IEqualityComparer&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1) by comparing individual equality components from child class.

```csharp
public abstract class EqualityComparerBase<T> : , System.Collections.IEqualityComparer, 
```

#### Type Parameters

`T`<br>
The type of objects to compare.

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → EqualityComparer&lt;T&gt; → [EqualityComparerBase&lt;T&gt;](./proffer.equalitycomparerbase-1)<br>
Implements [IEqualityComparer](https://docs.microsoft.com/en-us/dotnet/api/system.collections.iequalitycomparer), IEqualityComparer&lt;T&gt;

## Methods

### **Equals(T, T)**

When overridden in a derived class, determines whether two objects of type T are equal.

```csharp
public bool Equals(T x, T y)
```

#### Parameters

`x` T<br>
The first object to compare.

`y` T<br>
The second object to compare.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

            true if the specified objects are equal; otherwise, false.

### **GetHashCode(T)**

Returns a hash code for this instance.

```csharp
public int GetHashCode(T obj)
```

#### Parameters

`obj` T<br>
The object.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

            A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.

### **GetEqualityComponents(T)**

Gets the object's components that participate in equality comparisons.

```csharp
protected abstract IEnumerable<object> GetEqualityComponents(T obj)
```

#### Parameters

`obj` T<br>
The object.

#### Returns

[IEnumerable&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable containing the properties values.
