namespace Providers.Templating
{
    public interface ITemplateProviderScope
    {
        ITemplate Compile(string templateContent);

        void RegisterPartial(string name, string template);
    }
}
