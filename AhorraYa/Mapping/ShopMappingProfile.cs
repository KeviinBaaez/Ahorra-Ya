using AhorraYa.Application.Dtos.Shop;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Shop, ShopResponseDto>()
                .ForMember(dest => dest.LocationAddress, 
                opt => opt.MapFrom(src => src.Location!.GetFullAddress()));
            CreateMap<ShopRequestDto, Shop>();
        }
    }
}
