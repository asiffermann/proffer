namespace Proffer.Email
{
    using Internal;

    /// <summary>
    /// The Proffer.Email options with providers.
    /// </summary>
    public class EmailOptions 
    {
        /// <summary>
        /// The default configuration section name.
        /// </summary>
        public const string DefaultConfigurationSectionName = "Email";

        /// <summary>
        /// Gets or sets the provider options.
        /// </summary>
        public EmailProviderOptions Provider { get; set; }

        /// <summary>
        /// Gets or sets the default sender email address.
        /// </summary>
        public EmailAddress DefaultSender { get; set; }

        /// <summary>
        /// Gets or sets the template storage key to load templates from.
        /// </summary>
        public string TemplateStorage { get; set; }

        /// <summary>
        /// Gets or sets the mockup options.
        /// </summary>
        public MockupOptions Mockup { get; set; } = new MockupOptions();
    }
}
