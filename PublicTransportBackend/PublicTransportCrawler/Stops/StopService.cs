using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops.DTO;

namespace PublicTransportCrawler.Stops;

class StopService : IStopService
{
    private readonly HttpClient _httpClient;

    public StopService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<Actual>> GetRondoGrunwaldzkieDataAsync()
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(Constants.RondoGrunwaldzkieBusesInfo),
            Method = HttpMethod.Get
        };
        var response = await _httpClient.SendAsync(request);
        var responseAsString = await response.Content.ReadAsStringAsync();
        var converted = StopResponse.FromJson(responseAsString);
        return converted.Actual;
    }
}