using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler
{
    [ExcludeFromCodeCoverage]
    public class PublicTransportCrawler
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStopService _stopService;
        private readonly HttpClient _client;

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory,
            IVehicleService service,
            IStopService stopService)
        {
            this._client = httpClientFactory.CreateClient();
            this._vehicleService = service;
            _stopService = stopService;
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
                    Console.WriteLine(x.ActualTime);
                    var actualTime = new TimeSpan(int.Parse(x.ActualTime.Split(':')[0]), 
                        int.Parse(x.ActualTime.Split(':')[1]),
                        0);
                    var plannedTime = new TimeSpan(int.Parse(x.PlannedTime.Split(':')[0]), 
                        int.Parse(x.PlannedTime.Split(':')[1]),
                        0);
                    var diff = actualTime - plannedTime;
                    Console.WriteLine($"Diff in minutes: {diff.TotalMinutes}");
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
