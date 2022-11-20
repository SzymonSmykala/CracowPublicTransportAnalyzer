using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PublicTransportCrawler.Storage.DTO;

namespace PublicTransportCrawler.Storage.Repositories;

internal class DelayDataRepository : IDelayDataRepository
{
    private readonly DataContext _context;

    public DelayDataRepository(IOptions<MyServerOptions> options)
    {
        _context = new DataContext(options.Value);
    }

    public async Task InsertSampleDataAsync()
    {
        _context.Add<DelayStorage>(new DelayStorage()
        {
            DelayInMinutes = 10,
            StopId = "XD",
            Timestamp = DateTime.Now,
            TripId = "123",
            id = Guid.NewGuid().ToString()
        });
        await _context.SaveChangesAsync();
    }
}