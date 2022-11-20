using System.Threading.Tasks;

namespace PublicTransportCrawler.Storage.Repositories;

public interface IDelayDataRepository
{
    Task InsertSampleDataAsync();
}