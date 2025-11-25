using AhorraYa.Application.Dtos.Category;
using AhorraYa.WebClient.ViewModels.Category;
using AutoMapper;

namespace AhorraYa.WebClient.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            LoadCategoryMapping();
        }

        private void LoadCategoryMapping()
        {
            CreateMap<CategoryRequestDto, CategoryEditVm>().ReverseMap();
            CreateMap<CategoryResponseDto, CategoryListVm>().ReverseMap();
        }
    }
}
