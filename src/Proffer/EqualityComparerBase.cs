namespace Proffer
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements <see cref="IEqualityComparer{T}"/> by comparing individual equality components from child class.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <seealso cref="EqualityComparer{T}" />
    public abstract class EqualityComparerBase<T> : EqualityComparer<T>
    {
        /// <summary>
        /// When overridden in a derived class, determines whether two objects of type T are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public override bool Equals(T x, T y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return this.GetEqualityComponents(x).SequenceEqual(this.GetEqualityComponents(y));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode(T obj)
        {
            return this.GetEqualityComponents(obj)
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return ( current * 23 ) + ( obj?.GetHashCode() ?? 0 );
                    }
                });
        }

        /// <summary>
        /// Gets the object's components that participate in equality comparisons.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>An enumerable containing the properties values.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents(T obj);
    }
}
