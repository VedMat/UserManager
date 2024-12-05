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
                .ForMember(dest => dest.StartupProgramId, opt => opt.MapFrom(src => src.StartupProgram.Id))
                .ForMember(dest => dest.StartupProgramName, opt => opt.MapFrom(src => src.StartupProgram.ProgramName))
                .ForMember(dest => dest.ContactIds, opt => opt.MapFrom(src => src.Contacts.Select(x => x.Id)));
            CreateMap<StartupProgramDto, StartupProgram>().ReverseMap();
            CreateMap<ContactDto, Contact>().ReverseMap()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.StartupId, opt => opt.MapFrom(src => src.Startup.Id))
                .ForMember(dest => dest.StartupName, opt => opt.MapFrom(src => src.Startup.Name));
            CreateMap<CompanyDto, Company>().ReverseMap()
                .ForMember(dest => dest.ContactIds, opt => opt.MapFrom(src => src.Contacts.Select(x => x.Id)));
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
