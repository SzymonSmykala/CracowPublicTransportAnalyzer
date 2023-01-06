using System.Collections.Generic;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops.DTO;

namespace PublicTransportCrawler.Stops;

public interface IStopService
{
    Task<List<Actual>> GetRondoGrunwaldzkieDataAsync();

    Task<List<Actual>> GetDataForStopByAsync(string stopId);
}