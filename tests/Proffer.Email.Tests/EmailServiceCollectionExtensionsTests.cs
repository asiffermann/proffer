namespace Proffer.Storage.Tests
{
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Email;
    using Proffer.Templating;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Email))]
    [Feature(nameof(EmailServiceCollectionExtensions))]
    public class EmailServiceCollectionExtensionsTests
    {
        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationSection()
        {
            string sectionName = "CustomEmailSection";
            string expectedProviderType = "Stub";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddHandlebars()
                        .AddEmail(fixture.Configuration.GetSection(sectionName))
                        .AddStubEmail();
                },
                new()
                {
                    { $"{sectionName}:Provider:Type", expectedProviderType }
                });

            IOptions<EmailOptions> emailOptions = fixture.Services.GetRequiredService<IOptions<EmailOptions>>();

            Assert.NotNull(emailOptions);
            Assert.NotNull(emailOptions.Value);
            Assert.NotNull(emailOptions.Value.Provider);
            Assert.Equal(expectedProviderType, emailOptions.Value.Provider.Type);

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            Assert.NotNull(emailSender);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationRoot()
        {
            string expectedProviderType = "Stub";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddHandlebars()
                        .AddEmail(fixture.Configuration)
                        .AddStubEmail();
                },
                new()
                {
                    { $"Storage:Stores:Templates:ProviderType", "FileSystem" },
                    { $"Email:Provider:Type", expectedProviderType }
                });

            IOptions<EmailOptions> emailOptions = fixture.Services.GetRequiredService<IOptions<EmailOptions>>();

            Assert.NotNull(emailOptions);
            Assert.NotNull(emailOptions.Value);
            Assert.NotNull(emailOptions.Value.Provider);
            Assert.Equal(expectedProviderType, emailOptions.Value.Provider.Type);

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            Assert.NotNull(emailSender);
        }

        [Fact]
        public void Should_ConfigureOptions_From_ConfigurationRoot_With_CustomSectionName()
        {
            string sectionName = "CustomEmailSection";
            string expectedProviderType = "Stub";
            var fixture = new SimpleServiceProviderFixture(
                (serviceProvider, fixture) =>
                {
                    serviceProvider
                        .AddStorage(fixture.Configuration)
                        .AddFileSystemStorage(Path.Combine(fixture.BasePath, "Stores"))
                        .AddTemplating()
                        .AddHandlebars()
                        .AddEmail(fixture.Configuration, sectionName)
                        .AddStubEmail();
                },
                new()
                {
                    { $"{sectionName}:Provider:Type", expectedProviderType }
                });

            IOptions<EmailOptions> emailOptions = fixture.Services.GetRequiredService<IOptions<EmailOptions>>();

            Assert.NotNull(emailOptions);
            Assert.NotNull(emailOptions.Value);
            Assert.NotNull(emailOptions.Value.Provider);
            Assert.Equal(expectedProviderType, emailOptions.Value.Provider.Type);

            IEmailSender emailSender = fixture.Services.GetRequiredService<IEmailSender>();
            Assert.NotNull(emailSender);
        }
    }
}
