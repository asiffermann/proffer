namespace Providers.Storage
{
    using Configuration;

    /// <summary>
    /// Injectable typed store, which allows to save, list or read files on a container in its configured <see cref="IStorageProvider"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of the store options.</typeparam>
    /// <seealso cref="IStore" />
    public interface IStore<TOptions> : IStore
        where TOptions : class, IStoreOptions, new()
    {
    }
}
