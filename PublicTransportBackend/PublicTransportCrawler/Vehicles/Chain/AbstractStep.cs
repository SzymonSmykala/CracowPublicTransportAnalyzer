using System.Threading.Tasks;

namespace PublicTransportCrawler.Vehicles.Chain;

public abstract class AbstractStep : IStepAdder
{
    protected IStep Next;

    public async Task ExecuteAsync(CrawlingContext context)
    {
        await ExecuteInnerAsync(context);
        if (Next != null)
        {
            await Next.ExecuteAsync(context);
        }
    }

    protected abstract Task ExecuteInnerAsync(CrawlingContext context);

    public void AddNext(IStepAdder step)
    {
        Next = step;
    }
}