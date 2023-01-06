using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage.Repositories;

public interface IVehicleDelayDataRepository
{
    Task AddAsync(VehicleDelayData vehicleDelayData);
    Task AddOrUpdateAsync(VehicleDelayData vehicleDelayData);
}