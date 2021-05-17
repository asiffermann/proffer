namespace Proffer.Storage.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Proffer.Configuration;

    /// <summary>
    /// Extensions methods to parse and bind options.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Parses the specified unparsed configuration.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="unparsedConfiguration">The unparsed configuration.</param>
        /// <returns>A typed dictionary with options binding from the given unparsed configuration.</returns>
        public static IReadOnlyDictionary<string, TOptions> Parse<TOptions>(this IReadOnlyDictionary<string, IConfigurationSection> unparsedConfiguration)
            where TOptions : class, INamedElementOptions, new()
        {
            if (unparsedConfiguration == null)
            {
                return new Dictionary<string, TOptions>();
            }

            return unparsedConfiguration
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => BindOptions<TOptions>(kvp));
        }

        /// <summary>
        /// Gets the store configuration.
        /// </summary>
        /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
        /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
        /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
        /// <param name="parsedOptions">The parsed options.</param>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="throwIfNotFound">If set to <c>true</c>, throws an exception if the store configuration is not found.</param>
        /// <returns>The typed store configuration.</returns>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        public static TStoreOptions GetStoreConfiguration<TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions> parsedOptions, string storeName, bool throwIfNotFound = true)
            where TInstanceOptions : class, IProviderOptions
            where TStoreOptions : class, IStoreOptions
            where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
        {
            parsedOptions.ParsedStores.TryGetValue(storeName, out TStoreOptions storeOptions);
            if (storeOptions != null)
            {
                return storeOptions;
            }

            if (throwIfNotFound)
            {
                throw new Exceptions.StoreNotFoundException(storeName);
            }

            return null;
        }

        /// <summary>
        /// Gets the scoped store configuration.
        /// </summary>
        /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
        /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
        /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
        /// <param name="parsedOptions">The parsed options.</param>
        /// <param name="storeName">The name of the store.</param>
        /// <param name="throwIfNotFound">If set to <c>true</c>, throws an exception if the store configuration is not found.</param>
        /// <returns>The typed scoped store configuration.</returns>
        /// <exception cref="Exceptions.StoreNotFoundException"></exception>
        public static TScopedStoreOptions GetScopedStoreConfiguration<TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions> parsedOptions, string storeName, bool throwIfNotFound = true)
            where TInstanceOptions : class, IProviderOptions
            where TStoreOptions : class, IStoreOptions
            where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
        {
            parsedOptions.ParsedScopedStores.TryGetValue(storeName, out TScopedStoreOptions scopedStoreOptions);
            if (scopedStoreOptions != null)
            {
                return scopedStoreOptions;
            }

            if (throwIfNotFound)
            {
                throw new Exceptions.StoreNotFoundException(storeName);
            }

            return null;
        }

        /// <summary>
        /// Computes the specified options.
        /// </summary>
        /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
        /// <typeparam name="TProviderOptions">The type of the provider instance options.</typeparam>
        /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
        /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
        /// <param name="providerOptions">The provider options.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        public static void Compute<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions>(this TProviderOptions providerOptions, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TProviderOptions, TStoreOptions, TScopedStoreOptions>
            where TProviderOptions : class, IProviderOptions, new()
            where TStoreOptions : class, IStoreOptions, new()
            where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
        {
            options.BindProviderOptions(providerOptions);
        }

        /// <summary>
        /// Computes the specified options.
        /// </summary>
        /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
        /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
        /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
        /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
        /// <param name="parsedStore">The parsed store options.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        public static void Compute<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this TStoreOptions parsedStore, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions>
            where TInstanceOptions : class, IProviderOptions, new()
            where TStoreOptions : class, IStoreOptions, new()
            where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
        {
            if (string.IsNullOrEmpty(parsedStore.FolderName))
            {
                parsedStore.FolderName = parsedStore.Name;
            }

            TInstanceOptions instanceOptions = null;
            if (!string.IsNullOrEmpty(parsedStore.ProviderName))
            {
                options.ParsedProviders.TryGetValue(parsedStore.ProviderName, out instanceOptions);
                if (instanceOptions == null)
                {
                    return;
                }

                parsedStore.ProviderType = instanceOptions.Type;
            }

            options.BindStoreOptions(parsedStore, instanceOptions);
        }

        /// <summary>
        /// Parses the store options.
        /// </summary>
        /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
        /// <typeparam name="TInstanceOptions">The type of the provider instance options.</typeparam>
        /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
        /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="options">The options.</param>
        /// <returns>The parsed store options.</returns>
        public static TStoreOptions ParseStoreOptions<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(this IStoreOptions storeOptions, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TInstanceOptions, TStoreOptions, TScopedStoreOptions>, new()
            where TInstanceOptions : class, IProviderOptions, new()
            where TStoreOptions : class, IStoreOptions, new()
            where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions
        {
            if (!( storeOptions is TStoreOptions parsedStoreOptions ))
            {
                // TODO: [https://github.com/asiffermann/proffer/issues/23] Add a warning log
                parsedStoreOptions = new TStoreOptions
                {
                    Name = storeOptions.Name,
                    ProviderName = storeOptions.ProviderName,
                    ProviderType = storeOptions.ProviderType,
                    AccessLevel = storeOptions.AccessLevel,
                    FolderName = storeOptions.FolderName,
                };
            }

            parsedStoreOptions.Compute<TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions>(options);
            return parsedStoreOptions;
        }

        private static TOptions BindOptions<TOptions>(KeyValuePair<string, IConfigurationSection> kvp)
            where TOptions : class, INamedElementOptions, new()
        {
            var options = new TOptions
            {
                Name = kvp.Key,
            };

            if (options is IStoreOptions storeOptions && string.IsNullOrEmpty(storeOptions.FolderName))
            {
                storeOptions.FolderName = options.Name;
            }

            ConfigurationBinder.Bind(kvp.Value, options);
            return options;
        }
    }
}
