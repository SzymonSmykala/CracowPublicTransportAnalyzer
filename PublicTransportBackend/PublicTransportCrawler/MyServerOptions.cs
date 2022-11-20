using System.Net.NetworkInformation;

namespace PublicTransportCrawler;

public class MyServerOptions
{
    public string CosmosDbUri { get; set; }
    public string CosmosDbKey { get; set; }
    public string DatabaseName { get; set; }
}