using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles.Chain.Steps;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles;

internal class LineCrawlerStepFactory : ILineCrawlerStepFactory
{
    private readonly IVehicleService _vehicleService;
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;
    private readonly IDelayCalculator _delayCalculator;
    private readonly IVehicleDelayDataRepository _vehicleDelayDataRepository;

    public LineCrawlerStepFactory(IVehicleService vehicleService,
        IVehiclePathService vehiclePathService,
        IStopService stopService,
        IDelayCalculator delayCalculator,
        IVehicleDelayDataRepository vehicleDelayDataRepository)
    {
        _vehicleService = vehicleService;
        _vehiclePathService = vehiclePathService;
        _stopService = stopService;
        _delayCalculator = delayCalculator;
        _vehicleDelayDataRepository = vehicleDelayDataRepository;
    }

    public IStep CreateGetAllBusesStep()
    {
        return new GetAllBusesStep(_vehicleService);
    }

    public IStep CreateQueryVehiclesStep()
    {
        return new QueryVehiclesStep();
    }

    public IStep CreateFetchDataAndSaveStep()
    {
        return new FetchDataAndSaveStep(_vehiclePathService, _stopService, _delayCalculator,
            _vehicleDelayDataRepository);
    }
}