namespace PublicTransportCrawler.Vehicles.Chain;

public interface IChainBuilder
{
    void Add(IStep step);
}