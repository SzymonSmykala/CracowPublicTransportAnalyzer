using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles.Chain.Steps;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles.Chain;

internal class LineCrawlerStepFactory : ILineCrawlerStepFactory
{
    private readonly IVehicleService _vehicleService;
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;
    private readonly IDelayCalculator _delayCalculator;
    private readonly IVehicleDelayDataRepository _vehicleDelayDataRepository;
    private readonly IVehicleDelayDataProvider _vehicleDelayDataProvider;

    public LineCrawlerStepFactory(IVehicleService vehicleService,
        IVehiclePathService vehiclePathService,
        IStopService stopService,
        IDelayCalculator delayCalculator,
        IVehicleDelayDataRepository vehicleDelayDataRepository,
        IVehicleDelayDataProvider vehicleDelayDataProvider)
    {
        _vehicleService = vehicleService;
        _vehiclePathService = vehiclePathService;
        _stopService = stopService;
        _delayCalculator = delayCalculator;
        _vehicleDelayDataRepository = vehicleDelayDataRepository;
        _vehicleDelayDataProvider = vehicleDelayDataProvider;
    }

    public IStepAdder CreateGetAllBusesStep()
    {
        return new GetAllBusesStep(_vehicleService);
    }

    public IStepAdder CreateQueryVehiclesStep()
    {
        return new QueryVehiclesStep();
    }

    public IStepAdder CreateFetchDataAndSaveStep()
    {
        return new FetchDataAndSaveStep(_vehicleDelayDataRepository, _vehicleDelayDataProvider);
    }
}