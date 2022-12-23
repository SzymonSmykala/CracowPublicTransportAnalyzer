using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles.Adapters;
using PublicTransportCrawler.Vehicles.Path;

namespace PublicTransportCrawler.Vehicles.Chain.Steps;

public class FetchDataAndSaveStep : AbstractStep
{
    private readonly IVehiclePathService _vehiclePathService;
    private readonly IStopService _stopService;
    private readonly IDelayCalculator _delayCalculator;
    private readonly IVehicleDelayDataRepository _vehicleDelayDataRepository;

    public FetchDataAndSaveStep(IVehiclePathService vehiclePathService, IStopService stopService, IDelayCalculator delayCalculator, IVehicleDelayDataRepository vehicleDelayDataRepository)
    {
        _vehiclePathService = vehiclePathService;
        _stopService = stopService;
        _delayCalculator = delayCalculator;
        _vehicleDelayDataRepository = vehicleDelayDataRepository;
    }

    protected override async Task ExecuteInnerAsync(CrawlingContext context)
    {
        var listOfResults = new List<VehicleDelayData>();
        foreach (var q in context.Vehicles)
        {
            var result = await CreateVehicleDelayData(q, context.LineNumber);
            await _vehicleDelayDataRepository.AddOrUpdateAsync(result);
            listOfResults.Add(result);
            Console.WriteLine(result);
        }
    }
    
    private async Task<VehicleDelayData> CreateVehicleDelayData(DTO.Vehicle one, string lineNumber)
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