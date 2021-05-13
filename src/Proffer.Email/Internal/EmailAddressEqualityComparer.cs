namespace Proffer.Email.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Supports the comparison of <see cref="IEmailAddress"/> for equality when the email address only should match.
    /// </summary>
    /// <seealso cref="EqualityComparerBase{IEmailAddress}" />
    public class EmailAddressEqualityComparer : EqualityComparerBase<IEmailAddress>
    {
        /// <summary>
        /// Gets the object's components that participate in equality comparisons.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// An enumerable containing the properties values.
        /// </returns>
        protected override IEnumerable<object> GetEqualityComponents(IEmailAddress obj)
        {
            yield return obj.Email;
        }
    }
}
