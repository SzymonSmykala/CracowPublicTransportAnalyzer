using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler
{
    [ExcludeFromCodeCoverage]
    public class PublicTransportCrawler
    {
        private readonly IVehicleService _vehicleService;
        private readonly HttpClient _client;

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory, IVehicleService service)
        {
            this._client = httpClientFactory.CreateClient();
            this._vehicleService = service;
        }

        [FunctionName("GetListOfVehicles")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await _vehicleService.GetAllVehicles();
            return new OkObjectResult(result);
        }
    }
}
