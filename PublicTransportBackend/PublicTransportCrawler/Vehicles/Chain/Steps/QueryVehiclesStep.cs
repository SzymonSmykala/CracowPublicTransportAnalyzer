using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain.Steps;

public class QueryVehiclesStep : AbstractStep
{
    protected override Task ExecuteInnerAsync(CrawlingContext context)
    {
        context.Vehicles = context.Vehicles.FindAll(x => x.Name != null && x.Name.Contains(context.LineNumber));
        return Task.CompletedTask;
    }
}