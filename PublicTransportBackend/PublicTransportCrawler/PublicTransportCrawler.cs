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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler
{
    [ExcludeFromCodeCoverage]
    public class PublicTransportCrawler
    {
        private readonly IVehicleService _vehicleService;
        private readonly HttpClient _client;
        private readonly IDelayDataRepository _delayDataRepository;
        private MyServerOptions _options;

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory, IVehicleService service, IDelayDataRepository delayDataRepository, IOptions<MyServerOptions> options)
        {
            this._client = httpClientFactory.CreateClient();
            this._vehicleService = service;
            _delayDataRepository = delayDataRepository;
            _options = options.Value;
        }

        [FunctionName("GetListOfVehicles")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await _vehicleService.GetAllVehicles();
            await _delayDataRepository.InsertSampleDataAsync();
            Console.WriteLine(_options);
            return new OkObjectResult(result);
        }
    }
}
