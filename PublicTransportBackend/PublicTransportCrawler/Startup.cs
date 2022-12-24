using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicTransportCrawler;
using PublicTransportCrawler.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Factories;
using PublicTransportCrawler.Vehicles;
using PublicTransportCrawler.Vehicles.Adapters;
using PublicTransportCrawler.Vehicles.Chain;
using PublicTransportCrawler.Vehicles.Factories;
using PublicTransportCrawler.Vehicles.Path;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PublicTransportCrawler;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IVehicleService, VehicleService>();
        builder.Services.AddSingleton<IVehicleRequestFactory, VehicleRequestFactory>();
        
        builder.Services.AddSingleton<MyServerOptions>();
        builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();
        builder.Services.AddSingleton<IDelayDataRepository, DelayDataRepository>();
        builder.Services.AddSingleton<IVehicleDelayDataRepository, VehicleDelayDataRepository>();

        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.AddAppConfiguration(configuration);
        builder.Services.AddSingleton<IStopService, StopService>();
        builder.Services.AddScoped<ICurrentVehicleStateFacade, CurrentVehicleStateFacade>();
        builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
        builder.Services.AddSingleton<IVehiclePathService, VehiclePathService>();
        builder.Services.AddSingleton<IStopDataRequestFactory, StopDataRequestFactory>();
        builder.Services.AddAutoMapper(typeof(VehicleDelayData), typeof(VehicleDelayStorage));
        builder.Services.AddSingleton<ILineCrawlerExecutor, LineCrawlerExecutor>();
        builder.Services.AddSingleton<ILineCrawlerStepFactory, LineCrawlerStepFactory>();
        builder.Services.AddSingleton<ITimeProvider, TimeProvider>();
    }
    
    private IConfiguration BuildConfiguration(string applicationRootPath)
    {
        var config =
            new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        return config;
    }
}

