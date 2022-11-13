using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Stops;

public interface IStopService
{
    Task<List<Actual>> GetRondoGrunwaldzkieDataAsync();
}