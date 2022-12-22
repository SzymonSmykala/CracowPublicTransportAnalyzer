using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface IStep
{
    Task ExecuteAsync(CrawlingContext context);
}