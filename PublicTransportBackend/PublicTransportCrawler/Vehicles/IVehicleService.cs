using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

internal interface IVehicleService
{
    Task<List<Vehicle>> GetAllVehicles();
}