namespace PublicTransportCrawler.Vehicles.Chain;

internal class ChainBuilder : IChainBuilder
{
    private IStepAdder _current;
    private IStep _head;
    
    public IChainBuilder Add(IStepAdder step)
    {
        if (_current == null)
        {
            _current = step;
            _head = step;
        }
        else
        {
            _current.AddNext(step);
            _current = step;
        }

        return this;
    }

    public IStep Build()
    {
        return _head;
    }
}