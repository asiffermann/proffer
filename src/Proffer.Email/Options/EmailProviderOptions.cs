namespace Proffer.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Generic options for a <see cref="IEmailProvider"/>.
    /// </summary>
    /// <seealso cref="IEmailProviderOptions" />
    public class EmailProviderOptions : IEmailProviderOptions
    {
        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the provider parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
