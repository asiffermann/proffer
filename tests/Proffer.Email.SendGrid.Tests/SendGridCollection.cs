namespace Proffer.Email.SendGrid.Tests
{
    using Proffer.Testing;
    using Xunit;

    [CollectionDefinition(nameof(SendGridCollection))]
    public class SendGridCollection : ServiceProviderCollection<SendGridFixture> { }
}
