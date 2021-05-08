namespace Proffer.Email.SendGrid.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(SendGridTestCollection))]
    public class SendGridTestCollection : ICollectionFixture<SendGridFixture> { }
}
