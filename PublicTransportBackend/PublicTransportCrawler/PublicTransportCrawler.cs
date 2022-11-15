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
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
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

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory,
            IVehicleService service,
            IStopService stopService,
            IDelayCalculator delayCalculator)
        {
            this._client = httpClientFactory.CreateClient();
            this._vehicleService = service;
            _stopService = stopService;
            _delayCalculator = delayCalculator;
        }

        [FunctionName("GetListOfVehicles")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await _vehicleService.GetAllVehicles();
            return new OkObjectResult(result);
        }
        
        [FunctionName("GetStopData")]
        public async Task<IActionResult> GetStopData(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await _stopService.GetRondoGrunwaldzkieDataAsync();

            var difference = result.Where(x => !string.IsNullOrEmpty(x.ActualTime)).ToList();
            
            difference.ForEach(x =>
            {
                try
                {
                    var diff = _delayCalculator.Execute(x);
                    Console.WriteLine($"Delay in minutes: {diff.TotalMinutes} | Line: {x.PatternText} | Direction: {x.Direction} ");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Message: {e.Message}");
                    Console.WriteLine($"Actual: {x.ActualTime} | Planned: {x.PlannedTime}");
                }
            
            });
            return new OkObjectResult(difference);
        }
    }
}
