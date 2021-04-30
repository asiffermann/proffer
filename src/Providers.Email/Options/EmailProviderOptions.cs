namespace Providers.Email
{
    using System.Collections.Generic;

    public class EmailProviderOptions : IEmailProviderOptions
    {
        public string Type { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
