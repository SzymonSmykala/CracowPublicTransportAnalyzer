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
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<IVehicleRequestFactory, VehicleRequestFactory>();
        
        builder.Services.AddScoped<MyServerOptions>();
        builder.Services.AddScoped<IDbContextFactory, DbContextFactory>();
        builder.Services.AddScoped<IDelayDataRepository, DelayDataRepository>();
        builder.Services.AddScoped<IVehicleDelayDataRepository, VehicleDelayDataRepository>();

        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.AddAppConfiguration(configuration);
        builder.Services.AddScoped<IStopService, StopService>();
        builder.Services.AddScoped<ICurrentVehicleStateFacade, CurrentVehicleStateFacade>();
        builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
        builder.Services.AddScoped<IVehiclePathService, VehiclePathService>();
        builder.Services.AddScoped<IStopDataRequestFactory, StopDataRequestFactory>();
        builder.Services.AddAutoMapper(typeof(VehicleDelayData), typeof(VehicleDelayStorage));
        builder.Services.AddScoped<ILineCrawlerExecutor, LineCrawlerExecutor>();
        builder.Services.AddScoped<ILineCrawlerStepFactory, LineCrawlerStepFactory>();
        builder.Services.AddScoped<ITimeProvider, TimeProvider>();
        builder.Services.AddScoped<IVehicleDelayDataProvider, VehicleDelayDataProvider>();
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

