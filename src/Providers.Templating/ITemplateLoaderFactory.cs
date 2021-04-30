namespace Providers.Templating
{
    using Storage;

    public interface ITemplateLoaderFactory
    {
        ITemplateLoader Create(IStore store);

        ITemplateLoader Create(IStore store, string scope);
    }
}
