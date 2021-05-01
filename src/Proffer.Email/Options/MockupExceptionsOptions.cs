namespace Proffer.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Options to exclude some emails or domains from a mockup configuration.
    /// </summary>
    public class MockupExceptionsOptions
    {
        /// <summary>
        /// Gets or sets the emails exclusions.
        /// </summary>
        public List<string> Emails { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the domains exclusions.
        /// </summary>
        public List<string> Domains { get; set; } = new List<string>();
    }
}
