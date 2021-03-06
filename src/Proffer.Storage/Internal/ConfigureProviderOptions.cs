namespace Proffer.Storage.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Options;
    using Proffer.Configuration;
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Configures a provider <typeparamref name="TParsedOptions"/> from generic <see cref="StorageOptions"/>.
    /// </summary>
    /// <typeparam name="TParsedOptions">The type of the parsed options.</typeparam>
    /// <typeparam name="TProviderOptions">The type of the provider instance options.</typeparam>
    /// <typeparam name="TStoreOptions">The type of the store options.</typeparam>
    /// <typeparam name="TScopedStoreOptions">The type of the scoped store options.</typeparam>
    /// <seealso cref="IConfigureOptions{TParsedOptions}" />
    public class ConfigureProviderOptions<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions> : IConfigureOptions<TParsedOptions>
        where TParsedOptions : class, IParsedOptions<TProviderOptions, TStoreOptions, TScopedStoreOptions>
        where TProviderOptions : class, IProviderOptions, new()
        where TStoreOptions : class, IStoreOptions, new()
        where TScopedStoreOptions : class, TStoreOptions, IScopedStoreOptions, new()
    {
        private readonly StorageOptions storageOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureProviderOptions{TParsedOptions, TInstanceOptions, TStoreOptions, TScopedStoreOptions}"/> class.
        /// </summary>
        /// <param name="storageOptions">The storage options.</param>
        public ConfigureProviderOptions(IOptions<StorageOptions> storageOptions)
        {
            this.storageOptions = storageOptions.Value;
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TParsedOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        /// <exception cref="Exceptions.BadProviderConfiguration"></exception>
        public void Configure(TParsedOptions options)
        {
            if (this.storageOptions == null)
            {
                return;
            }

            options.ConnectionStrings = this.storageOptions.ConnectionStrings;

            options.ParsedProviders = this.storageOptions.Providers.Parse<TProviderOptions>()
                .Where(kvp => kvp.Value.Type == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (KeyValuePair<string, TProviderOptions> providerOptions in options.ParsedProviders)
            {
                providerOptions.Value.Compute<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions>(options);
            }

            IReadOnlyDictionary<string, TStoreOptions> parsedStores = this.storageOptions.Stores.Parse<TStoreOptions>();
            foreach (KeyValuePair<string, TStoreOptions> parsedStore in parsedStores)
            {
                parsedStore.Value.Compute<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions>(options);
            }

            options.ParsedStores = parsedStores
                .Where(kvp => kvp.Value.ProviderType == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            IReadOnlyDictionary<string, TScopedStoreOptions> parsedScopedStores = this.storageOptions.ScopedStores.Parse<TScopedStoreOptions>();
            foreach (KeyValuePair<string, TScopedStoreOptions> parsedScopedStore in parsedScopedStores)
            {
                parsedScopedStore.Value.Compute<TParsedOptions, TProviderOptions, TStoreOptions, TScopedStoreOptions>(options);
            }

            options.ParsedScopedStores = parsedScopedStores
                .Where(kvp => kvp.Value.ProviderType == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
