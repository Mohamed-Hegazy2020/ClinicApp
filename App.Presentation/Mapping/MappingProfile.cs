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
            // Entity → ViewModel
            CreateMap<ApplicationUser, ApplicationUserModel>()
                .ForMember(dest => dest.SelectedRoles, opt => opt.Ignore())
                .ForMember(dest => dest.RolesDisplay, opt => opt.Ignore()); // ignore for ViewModel display

            // ViewModel → Entity
            CreateMap<ApplicationUserModel, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());

			CreateMap<Patient, PatientModel>().ReverseMap();
		


		}
	}
}
