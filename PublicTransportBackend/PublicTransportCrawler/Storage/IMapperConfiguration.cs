using AutoMapper;

namespace PublicTransportCrawler.Storage;

public interface IAutoMapperConfiguration
{
    IMapper GetMapper();
}