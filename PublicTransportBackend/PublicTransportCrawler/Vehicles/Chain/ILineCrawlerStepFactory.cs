namespace PublicTransportCrawler.Vehicles;

public interface ILineCrawlerStepFactory
{
    IStep CreateGetAllBusesStep();
    IStep CreateQueryVehiclesStep();
    IStep CreateFetchDataAndSaveStep();
}