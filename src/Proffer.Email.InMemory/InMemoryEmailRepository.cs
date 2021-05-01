namespace Proffer.Email.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A repository in memory to hold sent emails through the provider.
    /// </summary>
    /// <seealso cref="IInMemoryEmailRepository" />
    public class InMemoryEmailRepository : IInMemoryEmailRepository
    {
        private readonly List<InMemoryEmail> innerEmailStore = new();

        /// <summary>
        /// Gets the emails store.
        /// </summary>
        public IReadOnlyCollection<InMemoryEmail> Store => new ReadOnlyCollection<InMemoryEmail>(this.innerEmailStore);

        /// <summary>
        /// Saves the specified email in the store.
        /// </summary>
        /// <param name="email">The email.</param>
        public void Save(InMemoryEmail email) => this.innerEmailStore.Add(email);
    }
}
