namespace Proffer.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Options for a <see cref="IEmailProvider"/>.
    /// </summary>
    public interface IEmailProviderOptions
    {
        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the provider parameters.
        /// </summary>
        Dictionary<string, string> Parameters { get; set; }
    }
}
