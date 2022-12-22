namespace PublicTransportCrawler.Vehicles.Chain;

public class TemporaryStep : AbstractStep, IStepAdder            
{
    public void AddNext(IStepAdder step)
    {
        // _next = step;
    }
}