using System;
using System.Threading.Tasks;
using PublicTransportCrawler.Storage.Repositories;

namespace PublicTransportCrawler.Vehicles.Chain.Steps;

public class FetchDataAndSaveStep : AbstractStep
{
    private readonly IVehicleDelayDataRepository _vehicleDelayDataRepository;
    private readonly IVehicleDelayDataProvider _vehicleDelayDataProvider;

    public FetchDataAndSaveStep(IVehicleDelayDataRepository vehicleDelayDataRepository, IVehicleDelayDataProvider vehicleDelayDataProvider)
    {
        _vehicleDelayDataRepository = vehicleDelayDataRepository;
        _vehicleDelayDataProvider = vehicleDelayDataProvider;
    }

    protected override async Task ExecuteInnerAsync(CrawlingContext context)
    {
        foreach (var vehicle in context.Vehicles)
        {
            var result = await _vehicleDelayDataProvider.CreateVehicleDelayData(vehicle, context.LineNumber);
            await _vehicleDelayDataRepository.AddOrUpdateAsync(result);
            Console.WriteLine(result);
        }
    }
}