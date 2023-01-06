using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Vehicles.Chain;

public interface IVehicleDelayDataProvider
{
    Task<VehicleDelayData> CreateVehicleDelayData(DTO.Vehicle vehicle, string lineNumber);
}