namespace Proffer.Email
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Proffer.Email.Tests.Stubs;

    public static class StubEmailServiceCollectionExtensions
    {
        public static IServiceCollection AddStubEmail(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEmailProviderType, StubEmailProviderType>());
            return services;
        }
    }
}
