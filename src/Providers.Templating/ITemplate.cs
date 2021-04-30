namespace Providers.Templating
{
    public interface ITemplate
    {
        string Apply(object context);
    }
}
