using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.Chain;

namespace PublicTransportCrawler.Vehicles;

public interface IStep
{
    Task ExecuteAsync(CrawlingContext context);
}