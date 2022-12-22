using System.Collections.Generic;
using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Vehicles;

public interface ICurrentVehicleStateFacade
{
    Task<List<VehicleDelayData>> GetCurrentStateForAsync(int lineNumber);
}