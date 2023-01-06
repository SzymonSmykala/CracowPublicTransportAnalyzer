using PublicTransportCrawler.Vehicles.Chain;

namespace PublicTransportCrawler.Vehicles;

public interface ILineCrawlerStepFactory
{
    IStepAdder CreateGetAllBusesStep();
    IStepAdder CreateQueryVehiclesStep();
    IStepAdder CreateFetchDataAndSaveStep();
}