using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public class VehicleService : IVehicleService
{
    private HttpClient _httpClient;

    public VehicleService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<Vehicle>> GetAllVehicles()
    {
        HttpRequestMessage request = new HttpRequestMessage();
        request.RequestUri =
            new Uri("http://www.ttss.krakow.pl/internetservice/geoserviceDispatcher/services/vehicleinfo/vehicles?positionType=CORRECTED");
        request.Method = HttpMethod.Get;
        var result =  await _httpClient.SendAsync(request);
        var resultAsString = await result.Content.ReadAsStringAsync();
        Console.WriteLine(resultAsString);
        return null;
    }
}