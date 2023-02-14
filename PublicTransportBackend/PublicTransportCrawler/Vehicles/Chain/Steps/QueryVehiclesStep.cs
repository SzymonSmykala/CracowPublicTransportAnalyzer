using System;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain.Steps;

public class QueryVehiclesStep : AbstractStep
{
    protected override Task ExecuteInnerAsync(CrawlingContext context)
    {
        context.Vehicles = context.Vehicles.FindAll(x => x.Name != null && x.Name.Contains(context.LineNumber));
        // Console.WriteLine($"Found {context.Vehicles.Count} vehicles for {context.LineNumber}");
        return Task.CompletedTask;
    }
}