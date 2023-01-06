namespace PublicTransportCrawler.Vehicles.Chain;

public interface IChainBuilder
{
    IChainBuilder Add(IStepAdder step);
    IStep Build();
}