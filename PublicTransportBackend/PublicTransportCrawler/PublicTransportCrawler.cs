using System;
using System.Collections.Generic;
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
using PublicTransportCrawler.Stops;
using PublicTransportCrawler.Stops.Adapters;
using PublicTransportCrawler.Storage.Repositories;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler
{
    [ExcludeFromCodeCoverage]
    public class PublicTransportCrawler
    {
        private readonly IStopService _stopService;
        private readonly IDelayCalculator _delayCalculator;
        private readonly HttpClient _client;
        private readonly IDelayDataRepository _delayDataRepository;
        private readonly MyServerOptions _options;
        private readonly ILineCrawlerExecutor _lineCrawlerExecutor;

        public PublicTransportCrawler(IHttpClientFactory httpClientFactory,
            IDelayDataRepository delayDataRepository,
            IOptions<MyServerOptions> options,
            IStopService stopService,
            IDelayCalculator delayCalculator,
            ILineCrawlerExecutor lineCrawlerExecutor)
        {
            _client = httpClientFactory.CreateClient();
            _delayDataRepository = delayDataRepository;
            _stopService = stopService;
            _delayCalculator = delayCalculator;
            _lineCrawlerExecutor = lineCrawlerExecutor;
            _options = options.Value;
        }

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
                    _delayDataRepository.AddOrUpdateDelayDataAsync(x.TripId, "3338", diff, x.PatternText, x.Direction).GetAwaiter().GetResult();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Message: {e.Message}");
                    Console.WriteLine($"Actual: {x.ActualTime} | Planned: {x.PlannedTime}");
                }
            
            });
            return new OkObjectResult(difference);
        }

        [FunctionName("DelayCrawlerV2TimeTriggered")]
        public async Task RunTriggerAsync([TimerTrigger("*/5 4-22 * * *")] TimerInfo myTimer, ILogger log)
        {
            var tasks = new List<Task>
            {
                _lineCrawlerExecutor.ExecuteAsync("173"),
                _lineCrawlerExecutor.ExecuteAsync("194"),
                _lineCrawlerExecutor.ExecuteAsync("307"),
                _lineCrawlerExecutor.ExecuteAsync("182"),
            };

            await Task.WhenAll(tasks);
        }
    }
}
