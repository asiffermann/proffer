namespace Proffer.Email.SendGrid.Test
{
    using Proffer.Testing;
    using Xunit;

    [CollectionDefinition(nameof(SendGridCollection))]
    public class SendGridCollection : ServiceProviderCollection<SendGridFixture> { }
}
