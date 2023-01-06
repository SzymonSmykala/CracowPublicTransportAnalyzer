using System;
using System.Net.Http;

namespace PublicTransportCrawler.Stops;

internal class StopDataRequestFactory : IStopDataRequestFactory
{
    public HttpRequestMessage CreateStopDataRequest(string stopId)
    {
        return new HttpRequestMessage()
        {
            RequestUri = new Uri($"{Constants.BusesInfoEndpoint}{stopId}"),
            Method = HttpMethod.Get
        };
    }
}