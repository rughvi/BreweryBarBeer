using AutoMapper;
using Brewery_Bar_Beer.Data.DTOs;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {            
            CreateMap<BreweryDTO, BreweryResponse>();
            CreateMap<CreateBreweryRequest, BreweryDTO>().ForMember(dest => dest.Id, act => act.Ignore());
            CreateMap<UpdateBreweryRequest, BreweryDTO>();

            CreateMap<BeerDTO, BeerResponse>();
            CreateMap<CreateBeerRequest, BeerDTO>().ForMember(dest => dest.Id, act => act.Ignore());
            CreateMap<UpdateBeerRequest, BeerDTO>();

            CreateMap<BarDTO, BarResponse>();
            CreateMap<CreateBarRequest, BarDTO>().ForMember(dest => dest.Id, act => act.Ignore());
            CreateMap<UpdateBarRequest, BarDTO>();

            CreateMap<CreateBarBeerRequest, BarBeerDTO>()
                .ForMember(dest => dest.Id, act => act.Ignore());

            CreateMap<CreateBreweryBeerRequest, BreweryBeerDTO>()
                .ForMember(dest => dest.Id, act => act.Ignore());
        }
    }
}
