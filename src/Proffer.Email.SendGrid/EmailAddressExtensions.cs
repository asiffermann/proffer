namespace Proffer.Email.SendGrid
{
    using global::SendGrid.Helpers.Mail;

    /// <summary>
    /// <see cref="IEmailAddress"/> extension methods.
    /// </summary>
    internal static class EmailAddressExtensions
    {
        /// <summary>
        /// Converts an email address to its equivalent SendGrid model.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The SendGrid model for the email address.</returns>
        internal static EmailAddress ToSendGridEmail(this IEmailAddress email)
        {
            return new EmailAddress(email.Email, email.DisplayName);
        }
    }
}
