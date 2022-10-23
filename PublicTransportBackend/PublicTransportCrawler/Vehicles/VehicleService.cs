using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

internal class VehicleService : IVehicleService
{
    // private readonly IHttpClientFactory _httpClientFactory;
    
    public Task<List<Vehicle>> GetAllVehicles()
    {
        throw new System.NotImplementedException();
    }
}