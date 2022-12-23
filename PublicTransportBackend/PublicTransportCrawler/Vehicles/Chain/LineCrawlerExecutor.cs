using System.Threading.Tasks;
using PublicTransportCrawler.Vehicles.Chain;

namespace PublicTransportCrawler.Vehicles;

internal class LineCrawlerExecutor : ILineCrawlerExecutor
{
    private ILineCrawlerStepFactory _factory;

    public LineCrawlerExecutor(ILineCrawlerStepFactory factory)
    {
        _factory = factory;
    }

    public async Task ExecuteAsync(string lineNumber)
    {
        var builder = new ChainBuilder();
        var firstStep  = builder.Build();
        var context = new CrawlingContext();
        await firstStep.ExecuteAsync(context);
    }
}