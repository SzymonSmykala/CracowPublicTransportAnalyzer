using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PublicTransportCrawler.Adapters;
using PublicTransportCrawler.Storage.Factories;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage.Repositories;

internal class VehicleDelayDataRepository : IVehicleDelayDataRepository
{
    private readonly IOptions<MyServerOptions> _myServerOptions;
    private readonly IDbContextFactory _dbContextFactory;
    private ITimeProvider _timeProvider;
    private readonly IMapper _mapper;
    
    public VehicleDelayDataRepository(IAutoMapperConfiguration _autoMapperConfiguration, IOptions<MyServerOptions> myServerOptions, IDbContextFactory dbContextFactory, ITimeProvider timeProvider)
    {
        _myServerOptions = myServerOptions;
        _dbContextFactory = dbContextFactory;
        _timeProvider = timeProvider;
        _mapper = new AutoMapperConfiguration().GetMapper();
    }
    
    public async Task AddAsync(VehicleDelayData vehicleDelayData)
    {
        var context = _dbContextFactory.Create();
        await context.AddAsync(vehicleDelayData);
        await context.SaveChangesAsync();
    }

    public async Task AddOrUpdateAsync(VehicleDelayData vehicleDelayData)
    {
        var _context = _dbContextFactory.Create();
        var current = await _context.VehicleDelayDataStorage.SingleOrDefaultAsync(x => x.TripId == vehicleDelayData.TripId);
        if (current == null)
        {
            var result = _mapper.Map<VehicleDelayData, VehicleDelayStorage>(vehicleDelayData);
            result.Timestamp = _timeProvider.GetCurrentTime();
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