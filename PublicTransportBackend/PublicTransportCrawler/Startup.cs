using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PublicTransportCrawler;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Vehicles;

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
        builder.Services.AddSingleton<IStopService, StopService>();
        builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
    }
}