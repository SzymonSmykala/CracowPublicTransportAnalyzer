using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
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

    public async Task AddOrUpdateDelayDataAsync(string tripId, string stopId, TimeSpan currentDelay, long lineNumber, string direction)
    {
        DelayStorage queried = await _context.DelayStorages.SingleOrDefaultAsync(x => x.TripId == tripId);

        if (queried == null)
        {
            await _context.DelayStorages.AddAsync(new DelayStorage()
            {
                TripId   = tripId,
                StopId = stopId,
                DelayInMinutes = (int) currentDelay.TotalMinutes,
                id = Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow,
                LineNumber = lineNumber,
                Direction = direction
            });
        }
        else
        {
            queried.DelayInMinutes = (int) currentDelay.TotalMinutes;
            _context.DelayStorages.Update(queried);
        }

        await _context.SaveChangesAsync();
    }
}