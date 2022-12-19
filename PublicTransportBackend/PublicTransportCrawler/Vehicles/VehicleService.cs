using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.DTO;

namespace PublicTransportCrawler.Vehicles;

public class VehicleService : IVehicleService
{
    private readonly HttpClient _httpClient;
    private readonly IVehicleRequestFactory _vehicleRequestFactory;

    public VehicleService(IHttpClientFactory httpClientFactory, IVehicleRequestFactory vehicleRequestFactory)
    {
        _vehicleRequestFactory = vehicleRequestFactory;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<DTO.Vehicle>> GetAllTramsAsync()
    {
        var request = _vehicleRequestFactory.CreateGetTramRequest();
        var result =  await _httpClient.SendAsync(request);
        var resultAsString = await result.Content.ReadAsStringAsync();
        var vehicleResponse = VehicleResponse.FromJson(resultAsString);
        return vehicleResponse.Vehicles.ToList();
    }

    public async Task<List<DTO.Vehicle>> GetAllBusesAsync()
    {
        var request = _vehicleRequestFactory.CreateGetBusesRequest();
        var result =  await _httpClient.SendAsync(request);
        var resultAsString = await result.Content.ReadAsStringAsync();
        var vehicleResponse = VehicleResponse.FromJson(resultAsString);
        return vehicleResponse.Vehicles.ToList();
    }
}