namespace Proffer.Events
{
    using System;

    /// <summary>
    /// An abstract base typed equatable event
    /// </summary>
    /// <seealso cref="IEquatable{EventBase}" />
    public abstract class EventBase : IEquatable<EventBase>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase"/> class.
        /// </summary>
        public EventBase()
        {
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(EventBase other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            if (this.Key == other.Key)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as EventBase);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(EventBase a, EventBase b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(EventBase a, EventBase b)
        {
            return !( a == b );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }
    }
}
