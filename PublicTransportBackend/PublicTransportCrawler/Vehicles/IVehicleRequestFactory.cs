using System.Net.Http;

namespace PublicTransportCrawler.Vehicles;

public interface IVehicleRequestFactory
{
    HttpRequestMessage CreateGetTramRequest();
}