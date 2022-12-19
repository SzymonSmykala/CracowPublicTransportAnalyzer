using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Path;

public interface IVehiclePathService
{
    Task<DTO.Path> GetPathForAsync(string tripId);
}