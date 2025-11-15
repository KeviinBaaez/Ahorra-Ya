using AhorraYa.Application.Dtos.Identity.Rols;
using AutoMapper;
using AhorraYa.Entities.MicrosoftIdentity;

namespace AhorraYa.WebApi.Mapping.Identity.Rol
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RolResponseDto>()
                .ForMember(dest => dest.Id, 
                opt=>opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
            CreateMap<RolRequestDto, Role>();
        }
    }
}
