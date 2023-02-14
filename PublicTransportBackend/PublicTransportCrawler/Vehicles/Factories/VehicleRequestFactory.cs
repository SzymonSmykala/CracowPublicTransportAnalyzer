using System;
using System.Net.Http;

namespace PublicTransportCrawler.Vehicles.Factories;

public class VehicleRequestFactory : IVehicleRequestFactory
{
    public HttpRequestMessage CreateGetTramRequest()
    {
        return new HttpRequestMessage()
        {
            RequestUri = new Uri(Constants.TramsEndpoint),
            Method = HttpMethod.Get
        };
    }

    public HttpRequestMessage CreateGetBusesRequest()
    {
        return new HttpRequestMessage()
        {
            RequestUri = new Uri(Constants.BusesEndpoint),
            Method = HttpMethod.Get
        };
    }
}