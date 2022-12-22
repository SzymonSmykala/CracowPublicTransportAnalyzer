using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage.Repositories;

internal class VehicleDelayDataRepository : IVehicleDelayDataRepository
{
    private readonly DataContext _context;

    public VehicleDelayDataRepository(IOptions<MyServerOptions> options)
    {
        _context = new DataContext(options.Value);
    }
    
    public async Task AddAsync(VehicleDelayData vehicleDelayData)
    {
        await _context.AddAsync(vehicleDelayData);
        await _context.SaveChangesAsync();
    }
}