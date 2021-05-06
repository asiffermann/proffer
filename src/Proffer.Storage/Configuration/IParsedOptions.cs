namespace Proffer.Storage.Configuration
{
    using System.Collections.Generic;
    using Proffer.Configuration;

    /// <summary>
    /// Typed options parsed from the dynamic configuration.
    /// </summary>
    /// <typeparam name="TProviderOptions">The type of the provider instance options.</typeparam>
    /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
    /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
    public interface IParsedOptions<TProviderOptions, TStoreOptions, TScopedStoreOptions>
        where TProviderOptions : class, IProviderOptions
        where TStoreOptions : class, IStoreOptions
        where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the parsed provider options.
        /// </summary>
        IReadOnlyDictionary<string, TProviderOptions> ParsedProviders { get; set; }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        IReadOnlyDictionary<string, TStoreOptions> ParsedStores { get; set; }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        IReadOnlyDictionary<string, TScopedStoreOptions> ParsedScopedStores { get; set; }

        /// <summary>
        /// Binds the provider options.
        /// </summary>
        /// <param name="providerOptions">The provider options.</param>
        void BindProviderOptions(TProviderOptions providerOptions);

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        void BindStoreOptions(TStoreOptions storeOptions, TProviderOptions providerInstanceOptions = null);
    }
}
