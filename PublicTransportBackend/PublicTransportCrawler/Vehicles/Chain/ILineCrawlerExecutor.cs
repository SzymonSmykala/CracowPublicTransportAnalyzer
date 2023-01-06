using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles;

public interface ILineCrawlerExecutor
{
    Task ExecuteAsync(string lineNumber);
}