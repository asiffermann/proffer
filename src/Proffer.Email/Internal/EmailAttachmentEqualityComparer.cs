namespace Proffer.Email.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

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
            yield return ComputeHashes(obj.Data);
            yield return obj.ContentType;
        }

        private static string ComputeHashes(byte[] buffer)
        {
            string contentMD5 = string.Empty;

            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(buffer);
                contentMD5 = Convert.ToBase64String(hash);
            }

            return contentMD5;
        }
    }
}
