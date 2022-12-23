namespace PublicTransportCrawler.Vehicles.Chain;

public interface IChainBuilder
{
    void Add(IStepAdder step);
    IStep Build();
}