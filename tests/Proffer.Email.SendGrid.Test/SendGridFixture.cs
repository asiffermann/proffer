namespace Proffer.Email.SendGrid.Test
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Email.Internal;
    using Proffer.Testing;
    using Storage;
    using Templating;

    public class SendGridFixture : ServiceProviderFixture
    {
        public const string FirstRecipient = "tests@proffer-dotnet.org";
        public const string SecondRecipient = "hello@proffer-dotnet.org";

        public SendGridFixture()
        {
            this.SendGridKey = this.Configuration["Email:Provider:Parameters:Key"];

            IOptions<EmailOptions> emailOptions = this.Services.GetRequiredService<IOptions<EmailOptions>>();
            this.DefaultSender = emailOptions.Value.DefaultSender;
        }

        public string SendGridKey { get; set; }

        public EmailAddress DefaultSender { get; set; }

        public IOptions<EmailOptions> GetOptions(string storeName = null)
        {
            return Options.Create(new EmailOptions
            {
                Provider = new EmailProviderOptions
                {
                    Type = "SendGrid",
                    Parameters = new Dictionary<string, string>
                    {
                        { "Key", this.SendGridKey }
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

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.BasePath)
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration)
                .AddSendGridEmail();
        }
    }
}
