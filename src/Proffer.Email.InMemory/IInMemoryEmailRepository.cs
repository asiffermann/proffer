namespace Proffer.Email.InMemory
{
    using System.Collections.Generic;

    /// <summary>
    /// A repository in memory to hold sent emails through the provider.
    /// </summary>
    public interface IInMemoryEmailRepository
    {
        /// <summary>
        /// Gets the emails store.
        /// </summary>
        IReadOnlyCollection<InMemoryEmail> Store { get; }

        /// <summary>
        /// Saves the specified email in the store.
        /// </summary>
        /// <param name="email">The email.</param>
        void Save(InMemoryEmail email);
    }
}
