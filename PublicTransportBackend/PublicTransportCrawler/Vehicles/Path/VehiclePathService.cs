using System;
using System.Net.Http;
using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.DTO;

namespace PublicTransportCrawler.Vehicles.Path;

internal class VehiclePathService : IVehiclePathService
{
    private readonly HttpClient _httpClient;

    public VehiclePathService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<DTO.Path> GetPathForAsync(string tripId)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri($"{Constants.BusesPathEndpoint}{tripId}"),
            Method = HttpMethod.Get
        };
        var result =  await _httpClient.SendAsync(request);
        var resultAsString = await result.Content.ReadAsStringAsync();
        var pathResponse = DTO.Path.FromJson(resultAsString);
        return pathResponse;
    }
}