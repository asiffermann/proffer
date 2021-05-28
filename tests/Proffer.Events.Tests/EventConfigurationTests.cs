namespace Proffer.Events.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Events.Tests.Stubs;
    using Proffer.Events.Tests.Stubs.Configuration;
    using Proffer.Testing;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Events))]
    [Feature(nameof(Configuration))]
    public class EventConfigurationTests
    {
        [Fact]
        public void Should_Validate_QueueOptions_Whitout_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IOptions<StubOptions> eventOptions = fixture.Services.GetRequiredService<IOptions<StubOptions>>();

            Assert.True(eventOptions.Value.QueueOptions.TryGetValue("EmailQueue", out StubQueueOptions emailQueueOptions));
            Assert.Equal("Stub", emailQueueOptions.ProviderType);
            Assert.Equal("StubProvider", emailQueueOptions.ProviderName);
            Assert.Empty(emailQueueOptions.Validate());

            Assert.True(eventOptions.Value.QueueOptions.TryGetValue("NotificationQueue", out StubQueueOptions notificationQueueOptions));
            Assert.Equal("Stub", notificationQueueOptions.ProviderType);
            Assert.Equal("StubProvider", notificationQueueOptions.ProviderName);
            Assert.Empty(notificationQueueOptions.Validate());

            Assert.True(eventOptions.Value.QueueOptions.TryGetValue("DenormalizationQueue", out StubQueueOptions denormalizationQueueOptions));
            Assert.Equal("Stub", denormalizationQueueOptions.ProviderType);
            Assert.Null(denormalizationQueueOptions.ProviderName);
            Assert.Empty(denormalizationQueueOptions.Validate());
        }

        [Fact]
        public void Should_Validate_ProvidersOptions_Whitout_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IOptions<StubOptions> eventOptions = fixture.Services.GetRequiredService<IOptions<StubOptions>>();

            Assert.True(eventOptions.Value.ProviderOptions.TryGetValue("StubProvider", out StubProviderOptions queueProviderOptions));

            Assert.Equal("Stub", queueProviderOptions.Type);
            Assert.Equal("StubProvider", queueProviderOptions.Name);
        }

        [Fact]
        public void Should_Not_Validate_QueueOptions_Whith_Errors()
        {
            var fixture = new SimpleServiceProviderFixture(
                (sp, f) => sp.AddEvent(f.Configuration).AddStubQueue());

            IOptions<StubOptions> eventOptions = fixture.Services.GetRequiredService<IOptions<StubOptions>>();

            IEnumerable<IOptionError> validationResult;

            eventOptions.Value.QueueOptions.TryGetValue("EmailQueue", out StubQueueOptions emailQueueOptions);
            emailQueueOptions.ProviderName = null;
            emailQueueOptions.ProviderType = null;
            validationResult = emailQueueOptions.Validate();
            Assert.NotEmpty(validationResult);
            Assert.Equal("Providers", validationResult.First().PropertyName);
            Assert.Equal("You should set either a ProviderType or a ProviderName for each Queue.", validationResult.First().ErrorMessage);

            emailQueueOptions.Name = null;
            validationResult = emailQueueOptions.Validate();
            Assert.NotEmpty(validationResult);
            Assert.Contains(validationResult, (error) => error.PropertyName == "Providers");
            Assert.Contains(validationResult, (error) => error.ErrorMessage == "You should set either a ProviderType or a ProviderName for each Queue.");
            Assert.Contains(validationResult, (error) => error.PropertyName == "Name");
            Assert.Contains(validationResult, (error) => error.ErrorMessage == "Name should be defined.");

            /*Assert.True(eventOptions.Value.QueueOptions.TryGetValue("NotificationQueue", out StubQueueOptions notificationQueueOptions));
            Assert.Equal("Stub", notificationQueueOptions.ProviderType);
            Assert.Equal("StubProvider", notificationQueueOptions.ProviderName);
            Assert.Empty(notificationQueueOptions.Validate());

            Assert.True(eventOptions.Value.QueueOptions.TryGetValue("DenormalizationQueue", out StubQueueOptions denormalizationQueueOptions));
            Assert.Equal("Stub", denormalizationQueueOptions.ProviderType);
            Assert.Null(denormalizationQueueOptions.ProviderName);
            Assert.Empty(denormalizationQueueOptions.Validate());*/
        }
    }
}
