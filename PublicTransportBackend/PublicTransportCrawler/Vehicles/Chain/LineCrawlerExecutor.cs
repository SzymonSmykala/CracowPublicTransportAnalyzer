using System;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain;

internal class LineCrawlerExecutor : ILineCrawlerExecutor
{
    private readonly ILineCrawlerStepFactory _stepFactory;

    public LineCrawlerExecutor(ILineCrawlerStepFactory stepFactory)
    {
        _stepFactory = stepFactory;
    }

    public async Task ExecuteAsync(string lineNumber)
    {
        // Console.WriteLine($"Executing for: {lineNumber}");
        var builder = new ChainBuilder();
        var firstStep = builder
            .Add(_stepFactory.CreateGetAllBusesStep())
            .Add(_stepFactory.CreateQueryVehiclesStep())
            .Add(_stepFactory.CreateFetchDataAndSaveStep())
            .Build();
        
        var context = new CrawlingContext
        {
            LineNumber = lineNumber
        };
        try
        {
            await firstStep.ExecuteAsync(context);
        }
        catch (Exception e)
        {
            Console.WriteLine($"{nameof(LineCrawlerExecutor)} failed. Error: {e.Message} Line: {lineNumber}");
        }
        

    }
}