namespace Proffer.Email
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A provider sends email using a particular messaging protocol or API.
    /// </summary>
    public interface IEmailProvider
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">All informations about the email.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(IEmail email);
    }
}
