namespace Providers.Storage.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// Typed options parsed from the dynamic configuration.
    /// </summary>
    /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
    /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
    /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
    public interface IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions>
        where TInstanceOptions : class, IProviderInstanceOptions
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
        /// Gets or sets the parsed provider instances options.
        /// </summary>
        IReadOnlyDictionary<string, TInstanceOptions> ParsedProviderInstances { get; set; }

        /// <summary>
        /// Gets or sets the parsed stores options.
        /// </summary>
        IReadOnlyDictionary<string, TStoreOptions> ParsedStores { get; set; }

        /// <summary>
        /// Gets or sets the parsed scoped stores options.
        /// </summary>
        IReadOnlyDictionary<string, TScopedStoreOptions> ParsedScopedStores { get; set; }

        /// <summary>
        /// Binds the provider instance options.
        /// </summary>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        void BindProviderInstanceOptions(TInstanceOptions providerInstanceOptions);

        /// <summary>
        /// Binds the store options.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="providerInstanceOptions">The provider instance options.</param>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        void BindStoreOptions(TStoreOptions storeOptions, TInstanceOptions providerInstanceOptions = null);
    }
}
