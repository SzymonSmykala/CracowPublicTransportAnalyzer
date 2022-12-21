using System;
using System.Threading.Tasks;

namespace PublicTransportCrawler.Storage.Repositories;

public interface IDelayDataRepository
{
    Task InsertSampleDataAsync();
    Task AddOrUpdateDelayDataAsync(string tripId, string stopId, TimeSpan currentDelay, long lineNumber, string direction);
}