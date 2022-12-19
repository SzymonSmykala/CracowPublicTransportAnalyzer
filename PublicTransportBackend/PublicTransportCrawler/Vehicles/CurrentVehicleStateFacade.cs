using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.DTO;
using PublicTransportCrawler.Vehicles.Adapters;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles;

internal class CurrentVehicleStateFacade : ICurrentVehicleStateFacade
{
    private readonly IVehicleService _vehicleService;
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;

    public CurrentVehicleStateFacade(IVehicleService vehicleService, IVehiclePathService vehiclePathService, IStopService stopService)
    {
        _vehicleService = vehicleService;
        _vehiclePathService = vehiclePathService;
        _stopService = stopService;
    }

    public async Task GetCurrentStateForAsync(int lineNumber)
    {
        // Get all vehicles
        var allVehicles = await _vehicleService.GetAllBusesAsync();
        // Query for lineNumber

        var queried = allVehicles.FindAll(x => x.Name != null && x.Name.Contains(lineNumber.ToString()));
        // Get timetables using tripIds
        var one = queried[0];

        var path = await _vehiclePathService.GetPathForAsync(one.TripId);
        var vehicleDelayData = new VehicleDelayData
        {
            Id = one.Id,
            TripId = one.TripId,
            Stops = new List<StopDelayData>()
        };

        foreach (var actual in path.Actual)
        {
            vehicleDelayData.Stops.Add(new StopDelayData(){StopId = actual.Stop.ShortName.ToString(), StopName = actual.Stop.Name});
        }
        
        // Call all stops from timetable to get delay data

        // foreach (var stop in vehicleDelayData.Stops)
        // {
        //     var stopData = await _stopService.GetDataForStopByAsync(stop.StopId);
        //     var data =  stopData.First(x => x.TripId == vehicleDelayData.TripId);
        //     stop.ActualTime = data.ActualTime;
        //     stop.ScheduleTime = data.PlannedTime;
        // }

        Parallel.ForEach(vehicleDelayData.Stops,  stop =>
        {
            var stopData = _stopService.GetDataForStopByAsync(stop.StopId).Result;
            var data = stopData.FirstOrDefault(x => x.TripId == vehicleDelayData.TripId);
            if (data != null)
            {
                stop.ActualTime = data.ActualTime;
                stop.ScheduleTime = data.PlannedTime;
            }
        });

        Console.WriteLine();
    }
}