using AhorraYa.Application.Dtos.Product;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductRequestDto, Product>();
        }
    }
}
