namespace Proffer.Email.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(EmailTestCollection))]
    public class EmailTestCollection : ICollectionFixture<EmailFixture> { }
}
