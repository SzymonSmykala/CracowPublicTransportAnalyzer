namespace PublicTransportCrawler.Vehicles.Chain;

internal class ChainBuilder : IChainBuilder
{
    private IStepAdder _current;
    
    public void Add(IStepAdder step)
    {
        if (_current == null)
        {
            _current = step;
        }
        else
        {
            _current.AddNext(step);
            _current = step;
        }
    }

    public IStep Build()
    {
        return _current;
    }
}