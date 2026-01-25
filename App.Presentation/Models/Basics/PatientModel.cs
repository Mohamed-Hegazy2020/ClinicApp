using App.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Presentation.Models.Basics
{
	public class PatientModel : BaseModel
	{
		[Required]
		[Display(Name = "NameAr")]
		public string? NameAr { get; set; }
		[Display(Name = "NameEn")]
		public string? NameEn { get; set; }
		[Display(Name = "DateOfBirth")]
		public DateTime DateOfBirth { get; set; }
		[Display(Name = "Age")]
		public int? Age { get; set; }
		[Required]
		[Display(Name = "Gender")]
		public string? Gender { get; set; }

		// Contact 
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string? Email { get; set; }
		[Display(Name = "PhoneNumber")]
		public string? PhoneNumber { get; set; }
		[Display(Name = "Address")]
		public string? Address { get; set; }

		// Medical Information 
		[Display(Name = "BloodType")]
		public string? BloodType { get; set; }
		/// <summary>
		/// لديه حساسية
		/// </summary>
		[Display(Name = "HasAllergies")]
		public bool HasAllergies { get; set; }
		/// <summary>
		/// تفاصيل الحساسية
		/// </summary>
		[Display(Name = "AllergiesDetails")]
		public string? AllergiesDetails { get; set; }
		/// <summary>
		/// يعاني من أمراض مزمنة
		/// </summary>
		[Display(Name = "HasChronicDiseases")]
		public bool HasChronicDiseases { get; set; }
		/// <summary>
		/// تفاصيل الأمراض المزمنة
		/// </summary>
		[Display(Name = "ChronicDiseasesDetails")]
		public string? ChronicDiseasesDetails { get; set; }
		/// <summary>
		/// الأدوية الحالية
		/// </summary>

		[Display(Name = "CurrentMedications")] 
		public string? CurrentMedications { get; set; }
		/// <summary>
		/// العمليات الجراحية السابقة
		/// </summary>
		[Display(Name = "PastSurgeries")]
		public string? PastSurgeries { get; set; }
		/// <summary>
		/// التاريخ الطبي للعائلة
		/// </summary>
		[Display(Name = "FamilyMedicalHistory")]
		public string? FamilyMedicalHistory { get; set; }

		public PatientModel()
		{
			NameEn = "";
			Email = "";
			PhoneNumber = "";
			Address = "";
			BloodType = "";
			AllergiesDetails = "";
			ChronicDiseasesDetails = "";
			CurrentMedications = "";
			PastSurgeries = "";
			FamilyMedicalHistory = "";
			
			
		}
	}
	
}
