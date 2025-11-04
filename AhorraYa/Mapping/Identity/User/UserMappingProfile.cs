using AhorraYa.Application.Dtos.Identity.User;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping.Identity.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterRequestDto, UserRegisterResponseDto>();
        }
    }
}
