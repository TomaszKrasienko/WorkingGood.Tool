using AutoMapper;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Shared.BrokerRoute;

namespace WorkingGood.Tool.Server.Mappers;

public class BrokerRouteMapperProfile : Profile
{
    public BrokerRouteMapperProfile()
    {
        CreateMap<BrokerQueue, BrokerQueueVM>();
        CreateMap<BrokerQueueDto, BrokerQueue>();
    }
}