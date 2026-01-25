using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace App.Presentation.Models.Basics
{
    public class ClinicModel:BaseModel
    {
    [Required]
		[Display(Name = "NameAr")]
		public string NameAr { get; set; }
		[Display(Name = "NameEn")]
		public string? NameEn { get; set; }
		[Display(Name = "Email")]
		public string? Email { get; set; }
		[Display(Name = "PhoneNumber")]
		public string? Phone { get; set; }
		[Required]
		[Display(Name = "Mobile")]
		public string? Mobile1 { get; set; }
		[Display(Name = "Mobile")]
		public string? Mobile2 { get; set; }
		[Required]
		[Display(Name = "Address")]
		public string? Address { get; set; }
		[Required]
		[Display(Name = "DoctorName")]
		public string? DoctorName { get; set; }
		[Required]
		[Display(Name = "WorkTimes")]
		public string? WorkTimes { get; set; }
		[Display(Name = "Fax")]
		public string? Fax { get; set; }
		[Display(Name = "Notes")]
		public string? Notes { get; set; }
		[Display(Name = "Image")]
		public string? ImagePath { get; set; }		
		[Ignore]
		[Display(Name = "Image")]
		public IFormFile? ImageFile { get; set; }

	}
}
