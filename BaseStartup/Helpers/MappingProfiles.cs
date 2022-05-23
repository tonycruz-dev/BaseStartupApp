using AutoMapper;
using BaseStartup.Dtos;
using BaseStartup.Entities;

namespace BaseStartup.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           CreateMap<AppUser, UserDto>().ReverseMap();

        }
    }
}
