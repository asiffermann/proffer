namespace Proffer.Events.Tests.Stubs.Configuration
{
    using System.Collections.Generic;
    using Proffer.Events.Configuration;

    public class StubOptions : IParsedOptions<StubProviderOptions, StubQueueOptions>
    {
        public string Name => StubEventProvider.ProviderName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public IReadOnlyDictionary<string, StubProviderOptions> ProviderOptions { get; set; }

        public IReadOnlyDictionary<string, StubQueueOptions> QueueOptions { get; set; }

        public void BindProviderOptions(StubProviderOptions providerOptions) { }

        public void BindQueueOptions(StubQueueOptions queueOptions, StubProviderOptions providerOptions = null) { }
    }
}
