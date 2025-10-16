using AhorraYa.Application.Dtos.Shop;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Shop, ShopResponseDto>();
            CreateMap<ShopRequestDto, Shop>();
        }
    }
}
