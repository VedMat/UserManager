using static System.Runtime.InteropServices.JavaScript.JSType;
using UserManager.DTOs;
using UserManager.Models;
using AutoMapper;

namespace UserManager.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<StartupDto, Startup>().ReverseMap()
                .ForMember(dest => dest.StartupProgramsId, opt => opt.MapFrom(src => src.StartupProgram.Id))
                .ForMember(dest => dest.StartupProgramsName, opt => opt.MapFrom(src => src.StartupProgram.ProgramName));
            CreateMap<StartupProgramDto, StartupProgram>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
