using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain.Steps;

public class GetAllBusesStep : AbstractStep
{
    private readonly IVehicleService _vehicleService;

    public GetAllBusesStep(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    protected override async Task ExecuteInnerAsync(CrawlingContext context)
    {
          context.Vehicles = await _vehicleService.GetAllBusesAsync();
    }
}