using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAllVehicles();
}