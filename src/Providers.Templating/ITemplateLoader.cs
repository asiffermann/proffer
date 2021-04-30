namespace Providers.Templating
{
    using System.Threading.Tasks;

    public interface ITemplateLoader
    {
        Task<ITemplate> GetTemplate(string name);
    }
}
