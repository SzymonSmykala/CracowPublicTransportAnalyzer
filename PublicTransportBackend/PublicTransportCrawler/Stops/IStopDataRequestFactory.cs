using System.Net.Http;

namespace PublicTransportCrawler.Stops;

public interface IStopDataRequestFactory
{
    HttpRequestMessage CreateStopDataRequest(string stopId);
}