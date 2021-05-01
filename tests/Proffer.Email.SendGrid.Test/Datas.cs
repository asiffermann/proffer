namespace Proffer.Email.Integration.Test
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;

    public class Datas
    {
        public const string FirstRecipient = "tests@proffer-dotnet.org";
        public const string SecondRecipient = "hello@proffer-dotnet.org";

        public static IOptions<EmailOptions> GetOptions(EmailServicesFixture storeFixture, string storeName = null)
        {
            return Options.Create(new EmailOptions
            {
                Provider = new EmailProviderOptions
                {
                    Type = "SendGrid",
                    Parameters = new Dictionary<string, string>
                    {
                        { "Key", storeFixture.SendGridKey }
                    },
                },
                TemplateStorage = storeName,
                DefaultSender = new Internal.EmailAddress
                {
                    DisplayName = "Proffer",
                    Email = "no-reply@proffer-dotnet.org"
                },
                Mockup = new MockupOptions
                {
                    Disclaimer = "",
                    Exceptions = new MockupExceptionsOptions
                    {
                        Domains = new List<string>(),
                        Emails = new List<string>(),
                    },
                    Recipients = new List<string>()
                }
            });
        }
    }
}
