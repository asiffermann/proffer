namespace Proffer.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Options to mockup the email sender (all recipients would be redirect to the mockup recipients).
    /// </summary>
    public class MockupOptions
    {
        /// <summary>
        /// Gets or sets the mockup recipients.
        /// </summary>
        public List<string> Recipients { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the exceptions options.
        /// </summary>
        public MockupExceptionsOptions Exceptions { get; set; } = new MockupExceptionsOptions();

        /// <summary>
        /// Gets or sets the disclaimer to add at the end of a mocked up email.
        /// </summary>
        public string Disclaimer { get; set; } = "This email was originally destined to the following recipients, and was mocked up because it was sent from a test environment.";
    }
}
