using System;
using System.Net.Http;

namespace PublicTransportCrawler.Vehicles;

class VehicleRequestFactory : IVehicleRequestFactory
{
    public HttpRequestMessage CreateGetTramRequest()
    {
        var request = new HttpRequestMessage();
        request.RequestUri =
            new Uri(Constants.TramsEndpoint);
        request.Method = HttpMethod.Get;
        return request;
    }
}