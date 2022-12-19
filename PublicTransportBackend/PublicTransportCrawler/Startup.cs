using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicTransportCrawler;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Vehicles;
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
        builder.Services.AddSingleton<IDelayDataRepository, DelayDataRepository>();
        
        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.AddAppConfiguration(configuration);
        builder.Services.AddSingleton<IStopService, StopService>();
        builder.Services.AddSingleton<ICurrentVehicleStateFacade, CurrentVehicleStateFacade>();
        builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
        builder.Services.AddSingleton<IVehiclePathService, VehiclePathService>();
        builder.Services.AddSingleton<IStopDataRequestFactory, StopDataRequestFactory>();
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

