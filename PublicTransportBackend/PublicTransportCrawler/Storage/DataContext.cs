using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PublicTransportCrawler.Storage.DTO;

namespace PublicTransportCrawler.Storage;

public class DataContext : DbContext
{
    private readonly MyServerOptions _options;

    public DataContext(MyServerOptions options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            _options.CosmosDbUri,
            _options.CosmosDbKey,
            _options.DatabaseName);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("data");
        modelBuilder.Entity<DelayStorage>().HasPartitionKey("StopId");
        modelBuilder.Entity<DelayStorage>().HasDiscriminator();
        // modelBuilder.Entity<DelayStorage>().Property(x => x.id).pr
    }
}