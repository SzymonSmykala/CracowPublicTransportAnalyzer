using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Stops.DTO;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles.Adapters;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles;

internal class CurrentVehicleStateFacade : ICurrentVehicleStateFacade
{
    private readonly IVehicleService _vehicleService;
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;
    private readonly IDelayCalculator _delayCalculator;
    private readonly IVehicleDelayDataRepository _vehicleDelayDataRepository;

    public CurrentVehicleStateFacade(IVehicleService vehicleService,
        IVehiclePathService vehiclePathService,
        IStopService stopService,
        IDelayCalculator delayCalculator,
        IVehicleDelayDataRepository vehicleDelayDataRepository)
    {
        _vehicleService = vehicleService;
        _vehiclePathService = vehiclePathService;
        _stopService = stopService;
        _delayCalculator = delayCalculator;
        _vehicleDelayDataRepository = vehicleDelayDataRepository;
    }

    public async Task<List<VehicleDelayData>> GetCurrentStateForAsync(int lineNumber)
    {
        // Get all vehicles
        var allVehicles = await _vehicleService.GetAllBusesAsync();
        // Query for lineNumber

        var queried = allVehicles.FindAll(x => x.Name != null && x.Name.Contains(lineNumber.ToString()));
        // Get timetables using tripIds

        var listOfResults = new List<VehicleDelayData>();
        foreach (var q in queried)
        {
            var result = await CreateVehicleDelayData(q, lineNumber);
            await _vehicleDelayDataRepository.AddOrUpdateAsync(result);
            listOfResults.Add(result);
            Console.WriteLine(result);
        }

        return listOfResults;
    }

    private async Task<VehicleDelayData> CreateVehicleDelayData(DTO.Vehicle one, int lineNumber)
    {
        var path = await _vehiclePathService.GetPathForAsync(one.TripId);
        var vehicleDelayData = new VehicleDelayData
        {
            Id = Guid.NewGuid().ToString(),
            TripId = one.TripId,
            Stops = new List<StopDelayData>(),
            LineNumber = lineNumber.ToString(),
        };

        foreach (var actual in path.Actual)
        {
            vehicleDelayData.Stops.Add(new StopDelayData(){StopId = actual.Stop.ShortName.ToString(), StopName = actual.Stop.Name});
        }
        Parallel.ForEach(vehicleDelayData.Stops,  stop =>
        {
            var stopData = _stopService.GetDataForStopByAsync(stop.StopId).Result;
            var data = stopData.FirstOrDefault(x => x.TripId == vehicleDelayData.TripId);
            if (data != null)
            {
                
                stop.ActualTime = data.ActualTime;
                stop.ScheduleTime = data.PlannedTime;
                stop.DelayInMinutes = _delayCalculator.Execute(stop.ActualTime, stop.ScheduleTime);
                vehicleDelayData.Direction = data.Direction;
            }
        });

        return vehicleDelayData;
    }
    
}