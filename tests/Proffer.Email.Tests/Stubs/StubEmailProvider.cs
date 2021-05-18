namespace Proffer.Email.Tests.Stubs
{
    using System;
    using System.Threading.Tasks;

    public class StubEmailProvider : IEmailProvider
    {

        public Task SendEmailAsync(IEmail email)
            => throw new NotImplementedException();
    }
}
