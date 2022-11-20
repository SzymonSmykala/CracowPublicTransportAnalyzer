using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler
{
    [ExcludeFromCodeCoverage]
    public class PublicTransportCrawler
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStopService _stopService;
        private readonly IDelayCalculator _delayCalculator;
        private readonly HttpClient _client;
        private readonly IDelayDataRepository _delayDataRepository;
        private MyServerOptions _options;

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory, IVehicleService service, IDelayDataRepository delayDataRepository, IOptions<MyServerOptions> options, IStopService stopService, IDelayCalculator delayCalculator)
        {
            this._client = httpClientFactory.CreateClient();
            this._vehicleService = service;
            _delayDataRepository = delayDataRepository;
            _stopService = stopService;
            _delayCalculator = delayCalculator;
            _options = options.Value;
        }

        // [FunctionName("GetListOfVehicles")]
        // public async Task<IActionResult> Run(
        //     [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        //     ILogger log)
        // {
        //     var result = await _vehicleService.GetAllVehicles();
        //     await _delayDataRepository.InsertSampleDataAsync();
        //     Console.WriteLine(_options);
        //     return new OkObjectResult(result);
        // }
        //
        [FunctionName("GetStopData")]
        public async Task<IActionResult> GetStopData(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await _stopService.GetRondoGrunwaldzkieDataAsync();

            var difference = result.Where(x => !string.IsNullOrEmpty(x.ActualTime)).ToList();
            
            difference.ForEach( x =>
            {
                try
                {
                    var diff = _delayCalculator.Execute(x);
                    Console.WriteLine($"Delay in minutes: {diff.TotalMinutes} | Line: {x.PatternText} | Direction: {x.Direction} ");
                    _delayDataRepository.AddOrUpdateDelayData(x.TripId, "3338", diff, x.PatternText, x.Direction).GetAwaiter().GetResult();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Message: {e.Message}");
                    Console.WriteLine($"Actual: {x.ActualTime} | Planned: {x.PlannedTime}");
                }
            
            });
            return new OkObjectResult(difference);
        }
        
        [FunctionName("DelayCrawler")]
        public async Task Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var result = await _stopService.GetRondoGrunwaldzkieDataAsync();
            
            var difference = result.Where(x => !string.IsNullOrEmpty(x.ActualTime)).ToList();
            
            difference.ForEach( x =>
            {
                try
                {
                    var diff = _delayCalculator.Execute(x);
                    Console.WriteLine($"Delay in minutes: {diff.TotalMinutes} | Line: {x.PatternText} | Direction: {x.Direction} ");
                    _delayDataRepository.AddOrUpdateDelayData(x.TripId, "3338", diff, x.PatternText, x.Direction).GetAwaiter().GetResult();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Message: {e.Message}");
                    Console.WriteLine($"Actual: {x.ActualTime} | Planned: {x.PlannedTime}");
                }
            
            });
        }
    }
}
