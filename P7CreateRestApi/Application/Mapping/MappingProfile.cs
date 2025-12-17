using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Application.Mapping
{
    // Profile AutoMapper : gère le mapping entre ViewModels et Entities 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BidListViewModel, BidList>().ReverseMap();
            CreateMap<CurvePointViewModel, CurvePoint>().ReverseMap();
            CreateMap<RatingViewModel, Rating>().ReverseMap();
            CreateMap<RuleViewModel, Rule>().ReverseMap();
            CreateMap<TradeViewModel, Trade>().ReverseMap();
        }
    }
}
