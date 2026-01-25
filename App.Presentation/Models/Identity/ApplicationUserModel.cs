using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace App.Presentation.Models.Identity
{
    public class ApplicationUserModel : IdentityUser<int>
    {
        public override DateTimeOffset? LockoutEnd { get; set; }        
        [PersonalData]
        public override bool TwoFactorEnabled { get; set; }        
        [PersonalData]
        public override bool PhoneNumberConfirmed { get; set; }       
        [ProtectedPersonalData]
        public override string? PhoneNumber { get; set; }        
        public override string? ConcurrencyStamp { get; set; }        
        public override string? SecurityStamp { get; set; }      
        public override string? PasswordHash { get; set; }       
        public override bool EmailConfirmed { get; set; }       
        public override string? NormalizedEmail { get; set; }      
        [ProtectedPersonalData]
        [Required]
        [Display(Name = "Email")]
        public override string? Email { get; set; }       
        public override string? NormalizedUserName { get; set; }      
        [ProtectedPersonalData]
        [Required]
        [Display(Name = "UserName")]
        public override string? UserName { get; set; }      
        [PersonalData]
        public override int Id { get; set; }       
        public override bool LockoutEnabled { get; set; }        
        public override int AccessFailedCount { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [DataType(dataType:DataType.Password)]
        [Ignore]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Ignore]
        [Display(Name = "RememberMe")]
        public bool IsPersistent { get; set; }
        [Required]
        [Display(Name = "Roles")]
        public List<string> SelectedRoles { get; set; }
        [Ignore]
        [Display(Name = "Roles")]
        public string? RolesDisplay { get; set; }    
        
        public IEnumerable<SelectListItem>? AvailableRoles { get; set; }
        public ApplicationUserModel()
        {
            SelectedRoles = new List<string>();
            AvailableRoles =new List<SelectListItem>();
        }
    }
}
