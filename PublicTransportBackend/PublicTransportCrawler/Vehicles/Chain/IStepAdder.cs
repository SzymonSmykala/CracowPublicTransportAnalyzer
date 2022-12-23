namespace PublicTransportCrawler.Vehicles.Chain;

public interface IStepAdder : IStep
{
    void AddNext(IStepAdder step);
}