namespace Proffer.Email
{
    /// <summary>
    /// An email address with its optional display name.
    /// </summary>
    public interface IEmailAddress
    {
        /// <summary>
        /// Gets the email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }
    }
}
