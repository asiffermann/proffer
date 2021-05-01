namespace Proffer.Email.Internal
{
    /// <summary>
    /// A simple email address with its optional display name.
    /// </summary>
    /// <seealso cref="IEmailAddress" />
    public class EmailAddress : IEmailAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        public EmailAddress()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="displayName">The display name.</param>
        public EmailAddress(string email, string displayName)
        {
            this.Email = email;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
