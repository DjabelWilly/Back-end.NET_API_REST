using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Application.Mapping
{
    // Profile AutoMapper : contient les règles de mapping entre ViewModels et Entities 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BidListViewModel, BidList>();
            CreateMap<TradeViewModel, Trade>();
        }
    }
}
