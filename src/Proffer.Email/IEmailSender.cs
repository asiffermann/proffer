namespace Proffer.Email
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends templated or raw emails using configured providers.
    /// </summary>
    public interface IEmailSender
    {

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">Everything about the mail.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendEmailAsync(IEmail email);

        /// <summary>
        /// Sends a templated email.
        /// </summary>
        /// <typeparam name="T">The type of context to apply on the template.</typeparam>
        /// <param name="email">Everything about the email.</param>
        /// <param name="context">The context  to apply on the template.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SendTemplatedEmailAsync<T>(IEmail email, T context);
    }
}
