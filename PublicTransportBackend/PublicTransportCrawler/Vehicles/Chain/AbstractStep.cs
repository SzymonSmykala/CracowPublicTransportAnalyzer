using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain;

public abstract class AbstractStep : IStep
{
    protected readonly IStep _next;

    public async Task ExecuteAsync(CrawlingContext context)
    {
        if (_next != null)
        {
            await _next.ExecuteAsync(context);
        }
    }
}