using AhorraYa.Application.Dtos.Location;
using AhorraYa.Entities;
using AutoMapper;

namespace AhorraYa.WebApi.Mapping
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationResponseDto>();
            CreateMap<LocationRequestDto, Location>();
        }
    }
}
