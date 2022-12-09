using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface ICurrentVehicleStateFacade
{
    Task GetCurrentStateForAsync(int lineNumber);
}

internal class CurrentVehicleStateFacade : ICurrentVehicleStateFacade
{
    public Task GetCurrentStateForAsync(int lineNumber)
    {
        // Get all vehicles
        
        // Query for lineNumber
        
        // Get timetables using tripIds
        
        // Call all stops from timetable to get delay data

        return Task.CompletedTask;
    }
}