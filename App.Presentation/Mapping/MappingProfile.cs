using App.Domain.Entities.Basics;
using App.Domain.Entities.Identity;
using App.Presentation.Models.Basics;
using App.Presentation.Models.Identity;
using AutoMapper;

namespace App.Presentation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Clinic, ClinicModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserModel>().ReverseMap();
      
        }
    }
}
