namespace PublicTransportCrawler.Storage.Factories;

public interface IDbContextFactory
{
    DbContext Create();
}