namespace Proffer.Email.Integration.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Storage;
    using Templating;

    public class StoresFixture
    {
        public StoresFixture()
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
        }

        public string SendGridKey { get; set; }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider Services { get; }

        public string BasePath { get; }
    }
}
