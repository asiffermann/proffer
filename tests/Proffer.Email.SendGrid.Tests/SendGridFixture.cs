namespace Proffer.Email.SendGrid.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Email.Internal;
    using Proffer.Testing;
    using Storage;
    using Templating;

    public class SendGridFixture : ServiceProviderFixtureBase
    {
        public const string FirstRecipient = "tests@getproffer.net";
        public const string SecondRecipient = "hello@getproffer.net";

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
                DefaultSender = new EmailAddress
                {
                    DisplayName = "Proffer",
                    Email = "no-reply@getproffer.net"
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
                .AddFileSystemStorage(Path.Combine(this.BasePath, "Stores"))
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration)
                .AddSendGridEmail();
        }
    }
}
