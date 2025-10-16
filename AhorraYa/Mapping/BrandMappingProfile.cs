using AhorraYa.Application.Dtos.Brand;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandResponseDto>();
            CreateMap<BrandRequestDto, Brand>();
        }
    }
}
