using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface ICurrentVehicleStateFacade
{
    Task GetCurrentStateForAsync(int lineNumber);
}