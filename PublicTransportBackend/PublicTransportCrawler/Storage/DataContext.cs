using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PublicTransportCrawler.Storage.DTO;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage;

public partial class DataContext : DbContext
{
    private readonly MyServerOptions _options;

    public virtual DbSet<DelayStorage> DelayStorages { get; set; }
    public virtual DbSet<VehicleDelayData> VehicleDelayDataStorage { get; set; }

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

        modelBuilder.Entity<VehicleDelayData>().HasDiscriminator();
        modelBuilder.Entity<VehicleDelayData>().HasPartitionKey(nameof(VehicleDelayData.PartitionKey));
        modelBuilder.Entity<VehicleDelayData>().ToContainer("vehicleDelay");
        // modelBuilder.Entity<DelayStorage>().Property(x => x.id).pr
    }
}