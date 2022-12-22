using AutoMapper;
using PublicTransportCrawler.Vehicles.Adapters;

namespace PublicTransportCrawler.Storage;

class AutoMapperConfiguration : IAutoMapperConfiguration
{
    private readonly MapperConfiguration _config;

    public AutoMapperConfiguration()
    {
        _config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleDelayData,VehicleDelayStorage>());
    }

    public IMapper GetMapper()
    {
        return _config.CreateMapper();
    }
}