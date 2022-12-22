namespace PublicTransportCrawler.Vehicles.Chain;

public interface IStepAdder
{
    void AddNext(IStepAdder step);
}