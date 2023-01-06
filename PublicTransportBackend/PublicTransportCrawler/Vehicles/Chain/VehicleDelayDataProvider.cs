using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Vehicles.Adapters;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles.Chain;

internal class VehicleDelayDataProvider : IVehicleDelayDataProvider
{
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;
    private readonly IDelayCalculator _delayCalculator;

    public VehicleDelayDataProvider(IDelayCalculator delayCalculator, IStopService stopService, IVehiclePathService vehiclePathService)
    {
        _delayCalculator = delayCalculator;
        _stopService = stopService;
        _vehiclePathService = vehiclePathService;
    }

    public async Task<VehicleDelayData> CreateVehicleDelayData(DTO.Vehicle vehicle, string lineNumber)
    {
        var vehicleDelayData = await InitializeVehicleDelayDataAsync(vehicle, lineNumber);
        await AssignMetadataToVehicleDelayDataAsync(vehicleDelayData);
        return vehicleDelayData;
    }
    
    private async Task AssignMetadataToVehicleDelayDataAsync(VehicleDelayData vehicleDelayData)
    {
        var tasks = vehicleDelayData.Stops.Select(async stop =>
        {
            var stopData = await _stopService.GetDataForStopByAsync(stop.StopId);
            var data = stopData.FirstOrDefault(x => x.TripId == vehicleDelayData.TripId);
            if (data != null)
            {
                stop.ActualTime = data.ActualTime;
                stop.ScheduleTime = data.PlannedTime;
                stop.DelayInMinutes = _delayCalculator.Execute(stop.ActualTime, stop.ScheduleTime);
                vehicleDelayData.Direction = data.Direction;
            }
        });
        await Task.WhenAll(tasks);
    }

    private async Task<VehicleDelayData> InitializeVehicleDelayDataAsync(DTO.Vehicle one, string lineNumber)
    {
        var vehicleDelayData = new VehicleDelayData
        {
            Id = Guid.NewGuid().ToString(),
            TripId = one.TripId,
            Stops = new List<StopDelayData>(),
            LineNumber = lineNumber,
        };

        var path = await _vehiclePathService.GetPathForAsync(one.TripId);

        foreach (var actual in path.Actual)
        {
            vehicleDelayData.Stops.Add(new StopDelayData()
                { StopId = actual.Stop.ShortName.ToString(), StopName = actual.Stop.Name });
        }

        return vehicleDelayData;
    }
}