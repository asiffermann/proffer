namespace Proffer.Email.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Supports the comparison of <see cref="IEmailAddress"/> for equality when both the email address and the display name should match.
    /// </summary>
    /// <seealso cref="EqualityComparerBase{IEmailAddress}" />
    public class EmailAddressStrictEqualityComparer : EqualityComparerBase<IEmailAddress>
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
            yield return obj.DisplayName;
            yield return obj.Email;
        }
    }
}
