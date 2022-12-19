using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface IVehicleService
{
    Task<List<DTO.Vehicle>> GetAllTramsAsync();
    Task<List<DTO.Vehicle>> GetAllBusesAsync();
}