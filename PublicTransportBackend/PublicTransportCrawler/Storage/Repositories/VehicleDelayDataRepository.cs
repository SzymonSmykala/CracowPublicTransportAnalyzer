using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage.Repositories;

internal class VehicleDelayDataRepository : IVehicleDelayDataRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public VehicleDelayDataRepository(IOptions<MyServerOptions> options, IAutoMapperConfiguration _autoMapperConfiguration)
    {
        _mapper = new AutoMapperConfiguration().GetMapper();
        _context = new DataContext(options.Value);
    }
    
    public async Task AddAsync(VehicleDelayData vehicleDelayData)
    {
        await _context.AddAsync(vehicleDelayData);
        await _context.SaveChangesAsync();
    }

    public async Task AddOrUpdateAsync(VehicleDelayData vehicleDelayData)
    {
        var current = await _context.VehicleDelayDataStorage.SingleOrDefaultAsync(x => x.TripId == vehicleDelayData.TripId);
        if (current == null)
        {
            var result = _mapper.Map<VehicleDelayData, VehicleDelayStorage>(vehicleDelayData);
            await _context.AddAsync(result);
        }
        else
        {
            foreach (var newStop in vehicleDelayData.Stops)
            {
                var stopToEdit = current.Stops.FirstOrDefault(x => x.StopId == newStop.StopId);
                if (stopToEdit == null)
                {
                    current.Stops.Add(newStop);
                }
                else
                {
                    stopToEdit.ActualTime = newStop.ActualTime;
                    stopToEdit.ScheduleTime = newStop.ScheduleTime;
                    stopToEdit.DelayInMinutes = newStop.DelayInMinutes;
                }
            }
            _context.VehicleDelayDataStorage.Update(current);
        }
    
        await _context.SaveChangesAsync();
    }
}