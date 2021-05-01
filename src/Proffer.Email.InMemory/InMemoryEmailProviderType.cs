namespace Proffer.Email.InMemory
{
    /// <summary>
    /// Builds <see cref="InMemoryEmailProvider"/>.
    /// </summary>
    /// <seealso cref="IEmailProviderType" />
    public class InMemoryEmailProviderType : IEmailProviderType
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => "InMemory";

        private readonly IInMemoryEmailRepository inMemoryEmailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEmailProviderType"/> class.
        /// </summary>
        /// <param name="inMemoryEmailRepository">The in-memory email repository.</param>
        public InMemoryEmailProviderType(IInMemoryEmailRepository inMemoryEmailRepository)
        {
            this.inMemoryEmailRepository = inMemoryEmailRepository;
        }

        /// <summary>
        /// Builds the provider.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        /// <returns>
        /// A new <see cref="IEmailProvider" />.
        /// </returns>
        public IEmailProvider BuildProvider(IEmailProviderOptions providerOptions)
            => new InMemoryEmailProvider(this.inMemoryEmailRepository);
    }
}
