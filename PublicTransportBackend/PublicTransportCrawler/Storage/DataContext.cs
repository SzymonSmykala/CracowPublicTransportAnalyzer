using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PublicTransportCrawler.Storage;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            _configuration.GetSection("CosmosDbUri").Value,
            _configuration.GetSection("CosmosDbKey").Value,
            databaseName: _configuration.GetSection("DatabaseName").Value);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Store");
    }
}