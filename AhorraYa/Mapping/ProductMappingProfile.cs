using AhorraYa.Application.Dtos.Product;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, 
                opt => opt.MapFrom(src => src.Category!.CategoryName))
                .ForMember(dest => dest.BrandName, 
                opt => opt.MapFrom(src => src.Brand!.BrandName))
                .ForMember(dest => dest.UnitName,
                opt => opt.MapFrom(src => src.MeasurementUnit!.UnitOfMeasure));
            CreateMap<ProductRequestDto, Product>();
        }
    }
}
