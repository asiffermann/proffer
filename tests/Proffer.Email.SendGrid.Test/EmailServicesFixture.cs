namespace Proffer.Email.Integration.Test
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;
    using Proffer.Email.Internal;
    using Storage;
    using Templating;

    public class EmailServicesFixture
    {
        public EmailServicesFixture()
        {
            this.BasePath = PlatformServices.Default.Application.ApplicationBasePath;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(this.BasePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
            this.SendGridKey = this.Configuration["Email:Provider:Parameters:Key"];

            var services = new ServiceCollection();

            services.AddMemoryCache();
            services.AddOptions();

            services
                .AddStorage(this.Configuration)
                .AddFileSystemStorage(this.BasePath)
                .AddTemplating()
                .AddHandlebars()
                .AddEmail(this.Configuration)
                .AddSendGridEmail();

            this.Services = services.BuildServiceProvider();

            IOptions<EmailOptions> emailOptions = this.Services.GetRequiredService<IOptions<EmailOptions>>();
            this.DefaultSender = emailOptions.Value.DefaultSender;
        }

        public string SendGridKey { get; set; }

        public EmailAddress DefaultSender { get; set; }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider Services { get; }

        public string BasePath { get; }
    }
}
