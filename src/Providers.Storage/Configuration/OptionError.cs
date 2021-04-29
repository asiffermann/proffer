namespace Providers.Storage.Configuration
{
    public class OptionError : IOptionError
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
