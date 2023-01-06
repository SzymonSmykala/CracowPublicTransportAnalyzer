using Microsoft.Extensions.Options;

namespace PublicTransportCrawler.Storage.Factories;

internal class DbContextFactory : IDbContextFactory
{
    private readonly IOptions<MyServerOptions> _options;

    public DbContextFactory(IOptions<MyServerOptions> options)
    {
        _options = options;
    }

    public DbContext Create()
    {
        return new DbContext(_options);
    }
}