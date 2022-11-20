using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PublicTransportCrawler;

internal static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MyServerOptions>(config.GetSection(nameof(MyServerOptions)));
        return services;
    }
}