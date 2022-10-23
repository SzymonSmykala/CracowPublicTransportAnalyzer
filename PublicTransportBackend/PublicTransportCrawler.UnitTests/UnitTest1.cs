using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using static TddXt.AnyRoot.Root;
namespace PublicTransportCrawler.UnitTests;

public class Tests
{
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        HttpRequest request = Any.Instance<HttpRequest>();
        ILogger logger = Any.Instance<ILogger>();
        await PublicTransportCrawler.Run(request, logger);
    }
}