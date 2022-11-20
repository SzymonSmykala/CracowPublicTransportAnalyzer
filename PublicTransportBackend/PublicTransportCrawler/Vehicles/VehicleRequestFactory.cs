using System;
using System.Net.Http;

namespace PublicTransportCrawler.Vehicles;

internal class VehicleRequestFactory : IVehicleRequestFactory
{
    public HttpRequestMessage CreateGetTramRequest()
    {
        return new HttpRequestMessage()
        {
            RequestUri = new Uri(Constants.TramsEndpoint),
            Method = HttpMethod.Get
        };
    }
}