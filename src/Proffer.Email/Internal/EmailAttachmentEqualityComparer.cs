namespace Proffer.Email.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Supports the comparison of <see cref="IEmailAttachment"/> for equality.
    /// </summary>
    /// <seealso cref="EqualityComparerBase{IEmailAttachment}" />
    public class EmailAttachmentEqualityComparer : EqualityComparerBase<IEmailAttachment>
    {
        /// <summary>
        /// Gets the object's components that participate in equality comparisons.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// An enumerable containing the properties values.
        /// </returns>
        protected override IEnumerable<object> GetEqualityComponents(IEmailAttachment obj)
        {
            yield return obj.FileName;
            yield return obj.Data;
            yield return obj.ContentType;
        }
    }
}
