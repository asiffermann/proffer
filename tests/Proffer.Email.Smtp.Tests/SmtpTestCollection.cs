namespace Proffer.Email.Smtp.Tests
{
    using Xunit;

    [CollectionDefinition(nameof(SmtpTestCollection))]
    public class SmtpTestCollection : ICollectionFixture<SmtpFixture> { }
}
