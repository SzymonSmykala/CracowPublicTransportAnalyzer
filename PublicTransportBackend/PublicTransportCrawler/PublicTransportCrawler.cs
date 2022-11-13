using System;
using System.Diagnostics.CodeAnalysis;
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

            var difference = result.Where(x => x.PlannedTime != x.ActualTime).ToList();
            Console.WriteLine(difference.ToString());
            return new OkObjectResult(difference);
        }
    }
}
