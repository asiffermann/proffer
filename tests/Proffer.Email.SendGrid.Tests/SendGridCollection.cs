namespace Proffer.Email.SendGrid.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(SendGridCollection))]
    public class SendGridCollection : ICollectionFixture<SendGridFixture> { }
}
