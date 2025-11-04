using AhorraYa.Application.Dtos.Identity.Rols;
using AutoMapper;
using AhorraYa.Entities.MicrosoftIdentity;

namespace AhorraYa.WebApi.Mapping.Identity.Rol
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RolResponseDto>();
            CreateMap<RolRequestDto, Role>();
        }
    }
}
