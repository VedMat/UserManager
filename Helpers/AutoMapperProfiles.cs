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
            CreateMap<ResourceDto, Resource>().ReverseMap();
        }
    }
}
